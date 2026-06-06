using Api.Base;
using Core.Features.Community.Posts.Commands.Models;
using Core.Features.Community.Posts.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : AppControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFeed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return NewResult(await Mediator.Send(new GetPostFeedQuery { PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new GetPostByIdQuery(id)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreatePostCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeletePostCommand(id)));
        }

        [HttpPut("{id}/moderate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Moderate([FromRoute] int id, [FromBody] ModeratePostCommand command)
        {
            command.Id = id;
            return NewResult(await Mediator.Send(command));
        }
    }
}
