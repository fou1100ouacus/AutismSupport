using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Base;
using Core.Features.Emails.Commands.Models;
using Data.AppMetaData;

namespace Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(Router.EmailsRoute.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
