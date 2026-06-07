using MediatR;
using Core.Bases;
using System.Text.Json.Serialization;

namespace Core.Features.ApplicationUser.Commands.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        // Id is set from token, not sent from client
        [JsonIgnore]
        public int Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
