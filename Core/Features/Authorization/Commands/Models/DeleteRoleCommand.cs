using MediatR;
using Core.Bases;
using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        [SwaggerSchema("The unique identifier of the role to delete")]
        public int Id { get; set; }
        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}
