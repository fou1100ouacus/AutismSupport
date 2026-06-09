using Data.Entities.Child;
using Data.Entities.Identity;
using Data.Entities.Enums;
using System;
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

        // تعديل: تم جعل الـ Foreign Key يقبل NULL (int?) لأن السطر مجمع للتقييم كله وليس لقسم واحد
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public virtual AbilityCategory? Category { get; set; }

        public int TotalScore { get; set; } // مجموع الدرجات الكلي للـ 15 سؤال

        // إضافة: العمود الجديد لحفظ النسبة المئوية الكلية (مثال: 65%) لعرضها في شاشة الـ UI الكبيرة
        public double? TotalPercentage { get; set; } 

        public string Level { get; set; } = string.Empty; // مستوى المخاطر الكلي (Low, Medium, High) أو (منخفض، متوسط، مرتفع)
        
        public DateTime TestDate { get; set; } = DateTime.UtcNow;
        
        // حفظ الإجابات التفصيلية ونتايع الأقسام كـ JSON للرجوع إليها في التقارير والـ History
        public string? DetailedAnswersJson { get; set; } 
    }
}