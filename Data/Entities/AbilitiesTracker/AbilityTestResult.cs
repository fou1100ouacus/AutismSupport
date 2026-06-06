using Data.Entities.Child;
using Data.Entities.Identity;
using Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.AbilitiesTracker
{
    public class AbilityTestResult
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Child))]
        public int ChildId { get; set; }
        public virtual ChildProfile Child { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual AbilityCategory Category { get; set; } = null!;

        public int TotalScore { get; set; } // مجموع الدرجات
        public string Level { get; set; } = string.Empty; // (Low, Medium, High)
        
        public DateTime TestDate { get; set; } = DateTime.UtcNow;
        
        // حفظ الإجابات التفصيلية للرجوع إليها في التقارير
        public string? DetailedAnswersJson { get; set; } 
    }
}