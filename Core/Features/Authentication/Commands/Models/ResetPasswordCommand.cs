using MediatR;
using Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace Core.Features.Authentication.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Code { get; set; }
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
