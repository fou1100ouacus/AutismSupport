using Api.Base;
using Core.Features.Community.Comments.Commands.Models;
using Core.Features.Community.Comments.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : AppControllerBase
    {
        [HttpGet("post/{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByPostId([FromRoute] int postId)
        {
            return NewResult(await Mediator.Send(new GetCommentsByPostIdQuery(postId)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeleteCommentCommand(id)));
        }

        [HttpPut("{id}/moderate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Moderate([FromRoute] int id, [FromBody] ModerateCommentCommand command)
        {
            command.Id = id;
            return NewResult(await Mediator.Send(command));
        }
    }
}