using MediatR;
using Core.Bases;

namespace Core.Features.Emails.Commands.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
