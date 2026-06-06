using Microsoft.AspNetCore.Mvc;
using Service.Abstracts;
using Core.AbilitiesTracker;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbilitiesController : ControllerBase
    {
        private readonly IAbilityService _abilityService;

        public AbilitiesController(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }
        [HttpGet("categoriesOnly")]
        public async Task<IActionResult> GetCategoriesOnly()
        {
            var categories = await _abilityService.GetAbilitiesCategoriesOnlyAsync();
            return Ok(categories);
        }
        // جلب أسئلة قسم معين عند ضغط الأم عليه
        [HttpGet("questions/{categoryId}")]
        public async Task<IActionResult> GetQuestions(int categoryId)
        {
            var questions = await _abilityService.GetQuestionsAsync(categoryId);
            
            if (questions == null || !questions.Any())
                return NotFound("لا توجد أسئلة لهذا القسم حالياً");

            return Ok(questions);
        }
      

        [HttpGet("child-history")]
        [Authorize] // تأكد من وجود صلاحية الوصول
        public async Task<IActionResult> GetChildHistory()
        {
            // 1. استخراج الـ UserId الخاص بالأم من الـ Token
            // استخدام الـ Id claim المخصص الذي يحتوي على المعرف الرقمي
            var motherIdStr = User.FindFirstValue("Id");
            if (string.IsNullOrEmpty(motherIdStr)) return Unauthorized();

            int motherId = int.Parse(motherIdStr);

            // 2. من خلال الـ Service، سنجلب التاريخ بناءً على الأم
            var history = await _abilityService.GetHistoryByMotherAsync(motherId);

            return Ok(history);
        }


        [HttpPost("submit-test")]
        [Authorize]
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestRequest request)
        {
            // استخراج معرف الأم من التوكن (الـ Claim الذي تستخدمه هو "Id")
            var motherIdStr = User.FindFirstValue("Id");
            if (string.IsNullOrEmpty(motherIdStr)) return Unauthorized();

            var result = await _abilityService.AddTestResultByMotherAsync(
                int.Parse(motherIdStr), 
                request.CategoryId, 
                request.Answers);

            if (result == "ChildNotFound")
                return BadRequest("لم يتم العثور على بروفايل طفل مرتبط بهذا الحساب.");

            return Ok(new { message = "تم حفظ التقييم بنجاح ونسبته لطفلك آلياً" });
        }



    }
}