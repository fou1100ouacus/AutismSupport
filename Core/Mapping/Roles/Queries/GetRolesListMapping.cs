using Core.Features.Authorization.Quaries.Results;
using Data.Entities.Identity;

namespace Core.Mapping.Roles
{
    public partial class RoleProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<Role, GetRolesListResult>();
        }
    }
}
