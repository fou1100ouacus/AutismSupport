using MediatR;
using Core.Bases;
using Core.Features.Authorization.Quaries.Results;

namespace Core.Features.Authorization.Quaries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {
    }
}
