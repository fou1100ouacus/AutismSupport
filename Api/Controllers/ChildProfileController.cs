// using Api.Base;
// using Core.Features.ChildProfile.Commands;
// using Core.Features.ChildProfile.Queries;
// using Core.Features.ChildProfile.Models;
// using MediatR;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace Api.Controllers
// {
//     /// <summary>
//     /// Controller for managing child profile operations.
//     /// Provides endpoints for creating, retrieving, and updating child profiles.
//     /// All endpoints require authentication.
//     /// </summary>
//     [Route("api/child-profiles")]
//     [ApiController]
//     [Authorize]
//     public class ChildProfileController : AppControllerBase
//     {
//         /// <summary>
//         /// Creates a new child profile for the authenticated user.
//         /// </summary>
//         /// <param name="dto">The child profile data transfer object containing profile details.</param>
//         /// <returns>
//         /// A response containing the created child profile data.
//         /// Returns 201 Created on success, or appropriate error status on failure.
//         /// </returns>
//         /// <response code="201">Profile created successfully</response>
//         /// <response code="400">Invalid request data</response>
//         /// <response code="401">User not authenticated</response>
//         /// <response code="500">Internal server error</response>
//         [HttpPost]
//         [ProducesResponseType(StatusCodes.Status201Created)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public async Task<IActionResult> CreateChildProfile([FromBody] CreateChildProfileDto dto)
//         {
//             var response = await Mediator.Send(new AddChildProfileCommand { Dto = dto });
//             return NewResult(response);
//         }

//         /// <summary>
//         /// Retrieves the child profile for the authenticated user.
//         /// </summary>
//         /// <returns>
//         /// A response containing the child profile data.
//         /// Returns 200 OK on success, or appropriate error status on failure.
//         /// </returns>
//         /// <response code="200">Profile retrieved successfully</response>
//         /// <response code="401">User not authenticated</response>
//         /// <response code="404">Profile not found</response>
//         /// <response code="500">Internal server error</response>
//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public async Task<IActionResult> GetProfile()
//         {
//             return NewResult(await Mediator.Send(new GetChildProfileQuery()));
//         }

//         /// <summary>
//         /// Updates the existing child profile for the authenticated user.
//         /// </summary>
//         /// <param name="dto">The child profile data transfer object containing updated profile details.</param>
//         /// <returns>
//         /// A response containing the updated child profile data.
//         /// Returns 200 OK on success, or appropriate error status on failure.
//         /// </returns>
//         /// <response code="200">Profile updated successfully</response>
//         /// <response code="400">Invalid request data</response>
//         /// <response code="401">User not authenticated</response>
//         /// <response code="404">Profile not found</response>
//         /// <response code="500">Internal server error</response>
//         [HttpPut]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status401Unauthorized)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public async Task<IActionResult> UpdateProfile([FromBody] CreateChildProfileDto dto)
//         {
//             return NewResult(await Mediator.Send(new UpdateChildProfileCommand { Dto = dto }));
//         }
//     }
// }


using Api.Base;
using Core.Features.ChildProfile.Commands;
using Core.Features.ChildProfile.Queries;
using Core.Features.ChildProfile.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Controller for managing child profile operations.
    /// Provides endpoints for creating, retrieving, and updating child profiles.
    /// All endpoints require authentication.
    /// </summary>
    [Route("api/child-profiles")]
    [ApiController]
    [Authorize]
    public class ChildProfileController : AppControllerBase
    {
        /// <summary>
        /// Creates a new child profile for the authenticated user.
        /// </summary>
        /// <param name="dto">The child profile data transfer object containing profile details.</param>
        /// <returns>
        /// A response containing the created child profile data.
        /// Returns 201 Created on success, or appropriate error status on failure.
        /// </returns>
        /// <response code="201">Profile created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateChildProfile([FromBody] CreateChildProfileDto dto)
        {
            var response = await Mediator.Send(new AddChildProfileCommand { Dto = dto });
            return NewResult(response);
        }

        /// <summary>
        /// Retrieves the child profile for the authenticated user.
        /// </summary>
        /// <returns>
        /// A response containing the child profile data.
        /// Returns 200 OK on success, or appropriate error status on failure.
        /// </returns>
        /// <response code="200">Profile retrieved successfully</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="404">Profile not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProfile()
        {
            return NewResult(await Mediator.Send(new GetChildProfileQuery()));
        }

        /// <summary>
        /// Updates the existing child profile for the authenticated user.
        /// </summary>
        /// <param name="dto">The child profile data transfer object containing updated profile details.</param>
        /// <returns>
        /// A response containing the updated child profile data.
        /// Returns 200 OK on success, or appropriate error status on failure.
        /// </returns>
        /// <response code="200">Profile updated successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="404">Profile not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProfile([FromBody] CreateChildProfileDto dto)
        {
            return NewResult(await Mediator.Send(new UpdateChildProfileCommand { Dto = dto }));
        }
    }
}