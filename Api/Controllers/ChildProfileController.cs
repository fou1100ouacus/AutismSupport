using Api.Base;
using Core.Features.ChildProfile.Commands;
using Core.Features.ChildProfile.Queries;
using Core.Features.ChildProfile.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ChildProfileController : AppControllerBase // 
    {
        [HttpPost]
        public async Task<IActionResult> CreateChildProfile([FromBody] CreateChildProfileDto dto)
        {
            // Mediator هنا موجود تلقائياً في AppControllerBase
            var response = await Mediator.Send(new AddChildProfileCommand { Dto = dto });
            return NewResult(response); 
        }

        [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            return NewResult(await Mediator.Send(new GetChildProfileQuery()));
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] CreateChildProfileDto dto)
        {
            return NewResult(await Mediator.Send(new UpdateChildProfileCommand { Dto = dto }));
}
    }
}
// namespace Api.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     [Authorize]   // عشان بس الأم المسجلة تقدر تضيف بروفايل
//     public class ChildProfileController : ControllerBase
//     {
//         private readonly IMediator _mediator;

//         public ChildProfileController(IMediator mediator)
//         {
//             _mediator = mediator;
//         }

//         // POST: api/ChildProfile
//         [HttpPost]
//         public async Task<IActionResult> CreateChildProfile([FromBody] CreateChildProfileDto dto)
//         {
//             if (dto == null)
//                 return BadRequest("Invalid data");

//             var command = new AddChildProfileCommand { Dto = dto };

//             var response = await _mediator.Send(command);

//             if (response.Succeeded)
//                 return Ok(response);

//             return BadRequest(response);
//         }
//     }
// }