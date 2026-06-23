using Swashbuckle.AspNetCore.Annotations;

namespace Data.DTOs
{
    public class EditRoleRequest
    {
        [SwaggerSchema("The unique identifier of the role to edit")]
        public int Id { get; set; }
        [SwaggerSchema("The new name for the role")]
        public string Name { get; set; }
    }
}
