using Api.Base; // عدلي الـ namespace للـ Base Controller الخاص بمشروعك
using Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //
    public class MotionAnalysisController : AppControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MotionAnalysisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // 1. الـ Endpoint الأول: رفع الفيديو (يستنتج الطفل تلقائياً ويرد بـ ID السجل فوراً)
        [HttpPost("analyze")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AnalyzeMotion([FromForm] AnalyzeChildMotionCommand command)
        {
            if (command.VideoFile == null || command.VideoFile.Length == 0)
                return BadRequest("Please provide a valid video file.");

            // 🕵️‍♂️ سحب الـ User ID الخاص بالأم من الـ Token Claims مستخدمة الـ Identity
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User context not found.");
            int motherUserId = int.Parse(userIdClaim);

            // 👶 جلب الـ ChildId المرتبط بحساب هذه الأم من الـ SQL Server تلقائياً
            var childId = await _context.ChildProfile
                .Where(c => c.MotherId == motherUserId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (childId == 0) 
                return BadRequest("No registered child found for this account. Please add a child profile first.");

            // 🎯 حقن الـ ChildId داخل الـ Command قبل إرساله للـ Handler
            command.AutomatedChildId = childId;

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
            var result = await _context.MotionAnalysisResults
                .Where(r => r.Id == id)
                .Select(r => new {
                    r.Status,       // سيعود بـ (Pending, Processing, Completed, Failed)
                    r.Prediction,   // النص النهائي للتصنيف
                    r.SmmPercentage,
                    r.SmmSegmentsCount,
                    r.VideoDuration
                })
                .FirstOrDefaultAsync();

            if (result == null) return NotFound("Analysis record not found.");
            return Ok(result);
        }
    }
}