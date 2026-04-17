using MediatR;
using Core.Bases;
using Data.DTOs;

namespace Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
