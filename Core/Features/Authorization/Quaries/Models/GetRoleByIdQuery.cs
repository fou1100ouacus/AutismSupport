using MediatR;
using Core.Bases;
using Core.Features.Authorization.Quaries.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Quaries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResult>>
    {
        [SwaggerSchema("The unique identifier of the role to retrieve")]
        public int Id { get; set; }
    }
}
