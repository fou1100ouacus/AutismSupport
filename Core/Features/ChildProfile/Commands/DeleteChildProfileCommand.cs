using MediatR;
using Core.Bases;

namespace Core.Features.ChildProfile.Commands
{
    public class DeleteChildProfileCommand : IRequest<Response<string>>
    {
    }
}
