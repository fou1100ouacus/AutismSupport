// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Api.Base;
// using Core.Features.Department.Queries.Models;
// using Data.AppMetaData;

// namespace Api.Controllers
// {
//     [ApiController]
//     [Authorize(Roles = "Admin")]
//     public class DepartmentController : AppControllerBase
//     {
//         [HttpGet(Router.DepartmentRouting.GetByID)]
//         public async Task<IActionResult> GetDepartmentByID([FromQuery] GetDepartmentByIDQuery query)
//         {
//             return NewResult(await Mediator.Send(query));
//         }
//         [HttpGet(Router.DepartmentRouting.GetDepartmentStudentsCount)]
//         public async Task<IActionResult> GetDepartmentStudentsCount()
//         {
//             return NewResult(await Mediator.Send(new GetDepartmentStudentListCountQuery()));
//         }

//         [HttpGet(Router.DepartmentRouting.GetDepartmentStudentsCountById)]
//         public async Task<IActionResult> GetDepartmentStudentsCountById([FromRoute] int id)
//         {
//             return NewResult(await Mediator.Send(new GetDepartmentStudentCountByIDQuery() { DID=id }));
//         }
//     }
// }
