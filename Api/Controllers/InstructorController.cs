
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Api.Base;
// using Core.Features.Instructors.Commands.Models;
// using Core.Features.Instructors.Queries.Models;
// using Data.AppMetaData;
// namespace Api.Controllers
// {
//     [ApiController]
//     [Authorize(Roles = "Admin")]
//     public class InstructorController : AppControllerBase
//     {
//         [HttpGet(Router.InstructorRouting.GetSalarySummationOfInstructor)]
//         public async Task<IActionResult> GetSalarySummation()
//         {
//             return NewResult(await Mediator.Send(new GetSummationSalaryOfInstructorQuery()));
//         }
//         [HttpPost(Router.InstructorRouting.AddInstructor)]
//         public async Task<IActionResult> AddInstructor([FromForm] AddInstructorCommand command)
//         {
//             return NewResult(await Mediator.Send(command));
//         }
//     }
// }
