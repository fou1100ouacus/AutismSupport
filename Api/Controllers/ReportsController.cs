using Api.Base;
using Core.Features.Community.Reports.Commands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ReportsController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
