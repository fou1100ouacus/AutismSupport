using MediatR;
using Infrastructure.Context;
using Data.Entities.AbilitiesTracker;
using Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Handlers
{
    public class AnalyzeChildMotionCommandHandler : IRequestHandler<AnalyzeChildMotionCommand, int>
    {
        private readonly ApplicationDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AnalyzeChildMotionCommandHandler> _logger;

        public AnalyzeChildMotionCommandHandler(
            ApplicationDBContext context, 
            HttpClient httpClient, 
            IServiceScopeFactory scopeFactory,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AnalyzeChildMotionCommandHandler> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _scopeFactory = scopeFactory;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<int> Handle(AnalyzeChildMotionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting motion analysis for video: {FileName}", request.VideoFile?.FileName);
                
                if (request.VideoFile == null || request.VideoFile.Length == 0)
                {
                    _logger.LogError("Video file is null or empty");
                    throw new ArgumentException("Video file is required");
                }
                
                // 1. سحب الـ User ID الخاص بالأم من الـ Token Claims
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userNameClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
                
                if (string.IsNullOrEmpty(userIdClaim) && string.IsNullOrEmpty(userNameClaim))
                {
                    _logger.LogError("User context not found in token");
                    throw new UnauthorizedAccessException("User context not found.");
                }
                
                int motherUserId = 0;
                
                // Try to parse userIdClaim as integer first
                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out motherUserId))
                {
                    _logger.LogInformation("User ID extracted from NameIdentifier: {UserId}", motherUserId);
                }
                // If that fails, try to get user by username
                else if (!string.IsNullOrEmpty(userNameClaim))
                {
                    _logger.LogInformation("NameIdentifier not numeric, using username: {UserName}", userNameClaim);
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userNameClaim, cancellationToken);
                    if (user == null)
                    {
                        _logger.LogError("User not found with username: {UserName}", userNameClaim);
                        throw new UnauthorizedAccessException("User not found.");
                    }
                    motherUserId = user.Id;
                    _logger.LogInformation("User ID extracted from username lookup: {UserId}", motherUserId);
                }
                else
                {
                    _logger.LogError("Cannot extract user ID from token claims");
                    throw new UnauthorizedAccessException("Cannot extract user ID from token claims.");
                }

            // 2. 👶 جلب الـ ChildId المرتبط بحساب هذه الأم تلقائياً
            var childId = await _context.ChildProfile
                .Where(c => c.MotherId == motherUserId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (childId == 0)
            {
                _logger.LogError("No child profile found for user: {UserId}", motherUserId);
                throw new InvalidOperationException("No registered child found for this account. Please add a child profile first.");
            }
            _logger.LogInformation("Child ID found: {ChildId}", childId);

            // 3. حفظ ملف الفيديو محلياً داخل فولدر wwwroot/videos لتوليد رابط ثابت
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.VideoFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            _logger.LogInformation("Saving video to: {FilePath}", filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.VideoFile.CopyToAsync(fileStream, cancellationToken);
            }
            var savedVideoUrl = $"/videos/{uniqueFileName}";
            _logger.LogInformation("Video saved successfully");

            // 4. تسجيل العملية في الـ SQL Server بحالة Pending باستخدام الـ ChildId المستنتج
            var analysisRecord = new MotionAnalysisResult
            {
                ChildId = childId,
                VideoUrl = savedVideoUrl,
                Status = "Pending"
            };

            await _context.MotionAnalysisResults.AddAsync(analysisRecord, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Analysis record created with ID: {AnalysisId}", analysisRecord.Id);

            // 3. 🚀 إطلاق الـ Background Task للاتصال بـ Hugging Face (Fire-and-Forget Pattern)
            var analysisIdLocal = analysisRecord.Id;
            var filePathLocal = filePath;
            var fileNameLocal = request.VideoFile.FileName;
            
            _ = Task.Run(async () => 
            {
                _logger.LogInformation("Background task started for analysis ID: {AnalysisId}", analysisIdLocal);
                
                try
                {
                    // فتح Scope مستقل وآمن للـ SQL Server لأن الـ HTTP Request الأصلي ينتهي فوراً
                    using var scope = _scopeFactory.CreateScope();
                    var bgContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                    var bgLogger = scope.ServiceProvider.GetRequiredService<ILogger<AnalyzeChildMotionCommandHandler>>();
                    
                    var trackedRecord = await bgContext.MotionAnalysisResults.FindAsync(analysisIdLocal);
                    if (trackedRecord == null)
                    {
                        bgLogger.LogError("Analysis record not found in background task: {AnalysisId}", analysisIdLocal);
                        return;
                    }

                    try
                    {
                        trackedRecord.Status = "Processing";
                        await bgContext.SaveChangesAsync();
                        bgLogger.LogInformation("Status updated to Processing for analysis ID: {AnalysisId}", analysisIdLocal);

                        // بناء الـ Multipart Form Data لمطابقة الـ Curl Request المتوقع من الـ Python API
                        using var responseContent = new MultipartFormDataContent();
                        
                        if (!File.Exists(filePathLocal))
                        {
                            bgLogger.LogError("Video file not found at: {FilePath}", filePathLocal);
                            trackedRecord.Status = "Failed";
                            trackedRecord.Prediction = "Video file not found";
                            await bgContext.SaveChangesAsync();
                            return;
                        }
                        
                        using var fileStreamForApi = File.OpenRead(filePathLocal);
                        using var streamContent = new StreamContent(fileStreamForApi);
                        
                        // "video" هو الـ المفتاح (Key) الذي ينتظره الـ AI Space الخاص بكِ
                        responseContent.Add(streamContent, "video", fileNameLocal);

                        var aiApiUrl = "https://asmaanasr11-asd-motion-detector.hf.space/analyze_video";
                        bgLogger.LogInformation("Sending request to AI server: {AiApiUrl}", aiApiUrl);
                        
                        // Create new HttpClient for background task
                        using var bgHttpClient = new HttpClient();
                        bgHttpClient.Timeout = TimeSpan.FromMinutes(5);
                        
                        var aiResponse = await bgHttpClient.PostAsync(aiApiUrl, responseContent);
                        bgLogger.LogInformation("AI server response status: {StatusCode}", aiResponse.StatusCode);

                        if (aiResponse.IsSuccessStatusCode)
                        {
                            var json = await aiResponse.Content.ReadAsStringAsync();
                            bgLogger.LogInformation("AI server response: {Json}", json);
                            
                            var result = JsonSerializer.Deserialize<HuggingFaceAiResponse>(json, new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                            
                            if (result != null && result.Report != null)
                            {
                                // تحديث السجل بالمقاييس الحقيقية المخزنة في الـ SQL Server
                                trackedRecord.Status = "Completed";
                                trackedRecord.SmmPercentage = result.Report.SmmPercentage;
                                trackedRecord.SmmSegmentsCount = result.Report.SmmSegmentsCount;
                                trackedRecord.TotalSegments = result.Report.TotalSegments;
                                trackedRecord.VideoDuration = result.Report.VideoDurationSeconds;
                                trackedRecord.Prediction = result.Report.SmmPercentage > 50.0 
                                    ? "High Stereotypic Motion Detected" 
                                    : "Normal Motion Activity";
                                
                                // Serialize segments to JSON and store in database
                                if (result.Segments != null && result.Segments.Count > 0)
                                {
                                    trackedRecord.SegmentsJson = JsonSerializer.Serialize(result.Segments, new JsonSerializerOptions
                                    {
                                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                                    });
                                    bgLogger.LogInformation("Segments stored: {Count} segments", result.Segments.Count);
                                }
                                
                                bgLogger.LogInformation("Analysis completed successfully. SmmPercentage: {SmmPercentage}, TotalSegments: {TotalSegments}", result.Report.SmmPercentage, result.Report.TotalSegments);
                            }
                            else
                            {
                                bgLogger.LogError("Failed to deserialize AI response or report is null");
                                trackedRecord.Status = "Failed";
                                trackedRecord.Prediction = "Failed to parse AI response";
                            }
                        }
                        else
                        {
                            var errorContent = await aiResponse.Content.ReadAsStringAsync();
                            bgLogger.LogError("AI server returned error: {StatusCode}, Content: {Content}", aiResponse.StatusCode, errorContent);
                            trackedRecord.Status = "Failed";
                            trackedRecord.Prediction = $"AI Server Error: {aiResponse.StatusCode}";
                        }
                    }
                    catch (Exception ex)
                    {
                        bgLogger.LogError(ex, "Exception in background task for analysis ID: {AnalysisId}", analysisIdLocal);
                        trackedRecord.Status = "Failed";
                        trackedRecord.Prediction = $"Exception: {ex.Message}";
                    }

                    // حفظ التحديثات النهائية ليعرف الموبايل أن النتيجة جاهزة
                    await bgContext.SaveChangesAsync();
                    bgLogger.LogInformation("Background task completed for analysis ID: {AnalysisId}", analysisIdLocal);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fatal error in background task for analysis ID: {AnalysisId}", analysisIdLocal);
                }
            });

            // 4. الرد الفوري بالـ ID للموبايل في أقل من ثانية دون انتظار الـ AI
            return analysisRecord.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Handle method: {Message}", ex.Message);
                throw;
            }
        }
    }
}