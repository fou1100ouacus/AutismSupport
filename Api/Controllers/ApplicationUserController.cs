using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Base;
using Core.Features.ApplicationUser.Commands.Models;
using Core.Features.ApplicationUser.Queries.Models;
using Data.AppMetaData;
using Service.AuthServices.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [AllowAnonymous]
    public class ApplicationUserController : AppControllerBase
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationUserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.ApplicationUserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.ApplicationUserRouting.GetByID)]
        public async Task<IActionResult> GetStudentByID([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPut(Router.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand command)
        {
            // Extract user ID from JWT token instead of receiving it in request body
            command.Id = _currentUserService.GetUserId();
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeleteUserCommand(id)));
        }
        [HttpPut(Router.ApplicationUserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            // Extract user ID from JWT token instead of receiving it in request body
            command.Id = _currentUserService.GetUserId();
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [Authorize]
        [HttpGet(Router.ApplicationUserRouting.GetMotherProfile)]
        public async Task<IActionResult> GetMotherProfile()
        {
            var response = await Mediator.Send(new GetMotherProfileQuery());
            return NewResult(response);
        }
    }
}
