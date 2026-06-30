using MediatR;
using Core.Bases;
using Core.Features.ApplicationUser.Queries.Results;

namespace Core.Features.ApplicationUser.Queries.Models
{
    public class GetMotherProfileQuery : IRequest<Response<GetMotherProfileResponse>>
    {
    }
}
