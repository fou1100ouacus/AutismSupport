using Core.Features.ChildProfile.Models;
using Core.Bases;       
using MediatR;

namespace Core.Features.ChildProfile.Queries
{
    public class GetChildProfileQuery : IRequest<Response<CreateChildProfileDto>>
    {
        // No parameters needed since we'll get user ID from token
    }
}
