using Swashbuckle.AspNetCore.Annotations;

namespace Data.Results
{
    public class ManageUserRolesResult
    {
        [SwaggerSchema("The unique identifier of the user")]
        public int UserId { get; set; }
        [SwaggerSchema("List of roles with their assignment status for the user")]
        public List<UserRoles> userRoles { get; set; }
    }
    public class UserRoles
    {
        [SwaggerSchema("The unique identifier of the role")]
        public int Id { get; set; }
        [SwaggerSchema("The name of the role")]
        public string Name { get; set; }
        [SwaggerSchema("Indicates whether the user has this role assigned")]
        public bool HasRole { get; set; }
    }
}
