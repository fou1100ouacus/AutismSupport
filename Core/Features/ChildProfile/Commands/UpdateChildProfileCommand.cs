using Core.Features.ChildProfile.Models;
using MediatR;
using Core.Bases;       
namespace Core.Features.ChildProfile.Commands
{
public class UpdateChildProfileCommand : IRequest<Response<string>>
{
    public CreateChildProfileDto Dto { get; set; } 

}}