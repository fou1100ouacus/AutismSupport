using Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Entities.Enums;
namespace Data.Entities.Child
{
    public class ChildProfile
    {
        [Key]
        public int Id { get; set; }

        // علاقة One-to-One مع الأم (User)
        [ForeignKey(nameof(Mother))]
        public int MotherId { get; set; }
        public virtual User Mother { get; set; } = null!;

        // Core Attributes
        public string? Nickname { get; set; }           // اسم الطفل أو اللقب
        public int AgeInYears { get; set; }
        public int AgeInMonths { get; set; }

        public Gender Gender { get; set; } = Gender.PreferNotToSay;

        public SupportNeedsLevel SupportNeedsLevel { get; set; } = SupportNeedsLevel.Medium;

        // يمكن تخزينها كـ JSON أو كـ Separate Table (ننصح بـ JSON في البداية)
        public string? MainDailyChallenges { get; set; }     // JSON string
        public string? StrengthsAndInterests { get; set; }   // JSON أو نص حر
        public bool PrefersVisualSchedules { get; set; } = false;

        public string? CommunicationMethods { get; set; }    // JSON string

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        // لمنع إنشاء أكثر من طفل لنفس الأمت)
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsActive { get; set; } = true;
    }
}