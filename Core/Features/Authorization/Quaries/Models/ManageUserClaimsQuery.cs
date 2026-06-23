using MediatR;
using Core.Bases;
using Data.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Quaries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        [SwaggerSchema("The unique identifier of the user to manage claims for")]
        public int UserId { get; set; }
    }
}
