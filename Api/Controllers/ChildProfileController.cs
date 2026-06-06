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
        [HttpPost("CreateChildProfile")]
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
