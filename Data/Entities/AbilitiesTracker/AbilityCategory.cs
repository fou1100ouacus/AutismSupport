using Data.Entities.Identity;
using Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities.AbilitiesTracker
{
    public class AbilityCategory
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty; // الاسم بالإنجليزي
        public string? Description { get; set; }
        
        // علاقة مع الأسئلة
        public virtual ICollection<AbilityQuestion> Questions { get; set; } = new List<AbilityQuestion>();
    }
}