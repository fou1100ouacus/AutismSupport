using MediatR;
using Core.Bases;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        [SwaggerSchema("The name of the role to create")]
        public string RoleName { get; set; }
    }
}
