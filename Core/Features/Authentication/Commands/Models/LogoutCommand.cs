using MediatR;
using Core.Bases;
using Data.Results;

namespace Core.Features.Authentication.Commands.Models
{
    public class LogoutCommand : IRequest<Response<string>>
    {
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
