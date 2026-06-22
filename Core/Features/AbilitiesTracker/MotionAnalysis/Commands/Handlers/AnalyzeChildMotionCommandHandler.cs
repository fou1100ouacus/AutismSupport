using MediatR;
using Infrastructure.Context;
using Data.Entities.AbilitiesTracker;
using Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Handlers
{
    public class AnalyzeChildMotionCommandHandler : IRequestHandler<AnalyzeChildMotionCommand, int>
    {
        private readonly ApplicationDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IServiceScopeFactory _scopeFactory;

        public AnalyzeChildMotionCommandHandler(ApplicationDBContext context, HttpClient httpClient, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _httpClient = httpClient;
            _scopeFactory = scopeFactory;
        }

        public async Task<int> Handle(AnalyzeChildMotionCommand request, CancellationToken cancellationToken)
        {
            // 1. حفظ ملف الفيديو محلياً داخل فولدر wwwroot/videos لتوليد رابط ثابت
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.VideoFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.VideoFile.CopyToAsync(fileStream, cancellationToken);
            }
            var savedVideoUrl = $"/videos/{uniqueFileName}";

            // 2. تسجيل العملية في الـ SQL Server بحالة Pending باستخدام الـ ChildId المستنتج
            var analysisRecord = new MotionAnalysisResult
            {
                ChildId = request.AutomatedChildId,
                VideoUrl = savedVideoUrl,
                Status = "Pending"
            };

            await _context.MotionAnalysisResults.AddAsync(analysisRecord, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // 3. 🚀 إطلاق الـ Background Task للاتصال بـ Hugging Face (Fire-and-Forget Pattern)
            _ = Task.Run(async () => 
            {
                // فتح Scope مستقل وآمن للـ SQL Server لأن الـ HTTP Request الأصلي ينتهي فوراً
                using var scope = _scopeFactory.CreateScope();
                var bgContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                
                var trackedRecord = await bgContext.MotionAnalysisResults.FindAsync(analysisRecord.Id);
                if (trackedRecord == null) return;

                try
                {
                    trackedRecord.Status = "Processing";
                    await bgContext.SaveChangesAsync();

                    // بناء الـ Multipart Form Data لمطابقة الـ Curl Request المتوقع من الـ Python API
                    using var responseContent = new MultipartFormDataContent();
                    using var fileStreamForApi = File.OpenRead(filePath);
                    using var streamContent = new StreamContent(fileStreamForApi);
                    
                    // "video" هو الـ المفتاح (Key) الذي ينتظره الـ AI Space الخاص بكِ
                    responseContent.Add(streamContent, "video", request.VideoFile.FileName);

                    var aiApiUrl = "https://asmaanasr11-asd-motion-detector.hf.space/analyze_video";
                    
                    // ضبط الـ Timeout ليكون 5 دقائق ليتناسب مع معالجة الـ CPU على Hugging Face
                    _httpClient.Timeout = TimeSpan.FromMinutes(5);
                    var aiResponse = await _httpClient.PostAsync(aiApiUrl, responseContent);

                    if (aiResponse.IsSuccessStatusCode)
                    {
                        var result = await aiResponse.Content.ReadFromJsonAsync<HuggingFaceAiResponse>();
                        if (result != null && result.Report != null)
                        {
                            // تحديث السجل بالمقاييس الحقيقية المخزنة في الـ SQL Server
                            trackedRecord.Status = "Completed";
                            trackedRecord.SmmPercentage = result.Report.Smm_Percentage;
                            trackedRecord.SmmSegmentsCount = result.Report.Smm_Segments_Count;
                            trackedRecord.VideoDuration = result.Report.Video_Duration_Seconds;
                            trackedRecord.Prediction = result.Report.Smm_Percentage > 50.0 
                                ? "High Stereotypic Linear Motion Detected" 
                                : "Normal Motion Activity";
                        }
                    }
                    else
                    {
                        trackedRecord.Status = "Failed";
                        trackedRecord.Prediction = "AI Server Error or Timeout";
                    }
                }
                catch (Exception ex)
                {
                    trackedRecord.Status = "Failed";
                    trackedRecord.Prediction = $"Exception: {ex.Message}";
                }

                // حفظ التحديثات النهائية ليعرف الموبايل أن النتيجة جاهزة
                await bgContext.SaveChangesAsync();
            });

            // 4. الرد الفوري بالـ ID للموبايل في أقل من ثانية دون انتظار الـ AI
            return analysisRecord.Id;
        }
    }
}