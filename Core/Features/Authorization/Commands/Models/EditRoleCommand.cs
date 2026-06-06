using MediatR;
using Core.Bases;
using Data.DTOs;

namespace Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
