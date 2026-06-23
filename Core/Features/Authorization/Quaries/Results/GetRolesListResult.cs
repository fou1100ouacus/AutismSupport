using Swashbuckle.AspNetCore.Annotations;

namespace Core.Features.Authorization.Quaries.Results
{
    public class GetRolesListResult
    {
        [SwaggerSchema("The unique identifier of the role")]
        public int Id { get; set; }
        [SwaggerSchema("The name of the role")]
        public string Name { get; set; }
    }
}
