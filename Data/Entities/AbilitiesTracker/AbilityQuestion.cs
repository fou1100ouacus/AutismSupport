
using Data.Entities.Identity;
using Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.AbilitiesTracker
{
    public class AbilityQuestion
    {
        [Key]
        public int Id { get; set; }
        public string QuestionTextAr { get; set; } = string.Empty;
        public string QuestionTextEn { get; set; } = string.Empty;
        
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public bool IsPositiveSkill { get; set; } = true;
        public virtual AbilityCategory Category { get; set; } = null!;
    }
}