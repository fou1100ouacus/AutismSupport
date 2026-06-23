using MediatR;
using Core.Bases;
using Data.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Quaries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        [SwaggerSchema("The unique identifier of the user to manage roles for")]
        public int UserId { get; set; }
    }
}
