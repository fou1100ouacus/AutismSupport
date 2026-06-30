using MediatR;
using Core.Bases;

namespace Core.Features.Authentication.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
