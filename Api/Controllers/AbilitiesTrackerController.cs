using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.Base; 
using Core.Features.AbilitiesTracker.Commands; 
using Core.Features.AbilitiesTracker; 

namespace Api.Controllers 
{
    public class AbilitiesTrackerController : AppControllerBase
    {
        // 1️⃣ Endpoint: جلب قائمة الأسئلة بالكامل للـ Mobile
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var response = await Mediator.Send(new GetAbilityQuestionsQuery());
            return NewResult(response); // بتغلف الـ Response تلقائياً بناءً على الـ StatusCode
        }

       [HttpPost("submit-test")]
        public async Task<IActionResult> SubmitTest([FromBody] SubmitTestRequestDto dto)
        {
            var response = await Mediator.Send(new SubmitTestCommand { Dto = dto });
            return NewResult(response);
        }

        // // 3️⃣ Endpoint: Get Test History
        // [HttpGet("history/{childId:int}")]
        // public async Task<IActionResult> GetTestHistory([FromRoute] int childId)
        // {
        //     var response = await Mediator.Send(new GetAbilityTestHistoryQuery { ChildId = childId });
        //     return NewResult(response);
        // }
    }
}