using Swashbuckle.AspNetCore.Annotations;

namespace Data.Results
{
    public class ManageUserClaimsResult
    {
        [SwaggerSchema("The unique identifier of the user")]
        public int UserId { get; set; }
        [SwaggerSchema("List of claims with their assignment status for the user")]
        public List<UserClaims> userClaims { get; set; }
    }
    public class UserClaims
    {
        [SwaggerSchema("The type of the claim (e.g., permission name)")]
        public string Type { get; set; }
        [SwaggerSchema("Indicates whether the user has this claim assigned")]
        public bool Value { get; set; }
    }
}
