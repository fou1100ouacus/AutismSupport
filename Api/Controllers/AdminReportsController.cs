using Api.Base;
using Core.Features.Community.Reports.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/admin/Reports")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminReportsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOpenReports()
        {
            return NewResult(await Mediator.Send(new GetOpenReportsQuery()));
        }
    }
}
