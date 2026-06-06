using Core.Features.ChildProfile.Models;
using MediatR;
using Core.Bases;        
namespace Core.Features.ChildProfile.Commands
{
    public class AddChildProfileCommand : IRequest<Response<int>>
    {
        public CreateChildProfileDto Dto { get; set; } = null!;
    }
}
