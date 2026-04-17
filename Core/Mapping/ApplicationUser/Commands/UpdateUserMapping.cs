using Core.Features.ApplicationUser.Commands.Models;
using Data.Entities.Identity;

namespace Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
