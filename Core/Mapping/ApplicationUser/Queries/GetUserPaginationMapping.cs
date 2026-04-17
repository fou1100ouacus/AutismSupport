using Core.Features.ApplicationUser.Queries.Results;
using Data.Entities.Identity;

namespace Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void GetUserPaginationMapping()
        {
            CreateMap<User, GetUserPaginationReponse>();

        }
    }
}