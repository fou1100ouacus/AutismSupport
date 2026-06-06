using Api.Base;
using Core.Features.Community.Moderation.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ModerationController : AppControllerBase
    {
        [HttpGet("queue")]
        public async Task<IActionResult> GetQueue()
        {
            return NewResult(await Mediator.Send(new GetModerationQueueQuery()));
        }
    }
}
