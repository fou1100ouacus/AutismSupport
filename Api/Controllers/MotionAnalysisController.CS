using Api.Base;
using Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Models;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotionAnalysisController : AppControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MotionAnalysisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. الـ Endpoint الأول: رفع الفيديو (يستنتج الطفل تلقائياً من الـ Token في الـ Handler)
        [HttpPost("analyze")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AnalyzeMotion([FromForm] AnalyzeChildMotionCommand command)
        {
            if (command.VideoFile == null || command.VideoFile.Length == 0)
                return BadRequest("Please provide a valid video file.");

            var analysisId = await Mediator.Send(command);
            return Ok(new { 
                Message = "Video uploaded successfully. Analysis started in background.", 
                AnalysisId = analysisId 
            });
        }

        // 2. الـ Endpoint الثاني: الموبايل بينادي عليه كل 10 ثوانٍ لمعرفة النتيجة وحالة الـ Background Job
        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetStatus(int id)
        {
            var record = await _context.MotionAnalysisResults
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (record == null) return NotFound("Analysis record not found.");

            // Build response in the format matching AI server response
            var response = new MotionAnalysisResponseDto
            {
                Report = new ReportDto
                {
                    SmmPercentage = record.SmmPercentage ?? 0,
                    SmmSegmentsCount = record.SmmSegmentsCount ?? 0,
                    TotalSegments = record.TotalSegments ?? 0,
                    VideoDurationSeconds = record.VideoDuration ?? 0
                },
                Segments = new List<SegmentDto>()
            };

            // Deserialize segments if available
            if (!string.IsNullOrEmpty(record.SegmentsJson))
            {
                try
                {
                    response.Segments = JsonSerializer.Deserialize<List<SegmentDto>>(record.SegmentsJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<SegmentDto>();
                }
                catch
                {
                    response.Segments = new List<SegmentDto>();
                }
            }

            return Ok(new
            {
                record.Status,
                record.Prediction,
                report = response.Report,
                segments = response.Segments
            });
        }
    }
}