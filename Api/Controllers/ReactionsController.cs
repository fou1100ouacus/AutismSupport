using Api.Base;
using Core.Features.Community.Reactions.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ReactionsController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Toggle([FromBody] ToggleReactionCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}