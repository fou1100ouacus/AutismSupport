using Data.Entities.Identity;
using Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Child
{
    public class ChildProfile
    {
        [Key]
        public int Id { get; set; }

        // One-to-One relationship with Mother (User)
        [ForeignKey(nameof(Mother))]
        public int MotherId { get; set; }

        public virtual User Mother { get; set; } = null!;

        // Core Attributes
        [Required(ErrorMessage = "Child nickname is required")]
        [MaxLength(100)]
        public string Nickname { get; set; } = string.Empty;

        [Range(0, 18)]
        public int AgeInYears { get; set; }

        [Range(0, 11)]
        public int AgeInMonths { get; set; }

        public Gender Gender { get; set; } = Gender.PreferNotToSay;

        public SupportNeedsLevel SupportNeedsLevel { get; set; } = SupportNeedsLevel.Medium;

        // JSON fields for lists (recommended approach)
        public string? MainDailyChallengesJson { get; set; }

        public string? StrengthsAndInterests { get; set; }

        public bool PrefersVisualSchedules { get; set; } = false;

        public string? CommunicationMethodsJson { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        // Helper property (not mapped to database)
        [NotMapped]
        public int TotalAgeInMonths => (AgeInYears * 12) + AgeInMonths;
    }
}