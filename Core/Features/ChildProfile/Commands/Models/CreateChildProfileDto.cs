using Data.Entities.Enums;

namespace Core.Features.ChildProfile.Models
{
    public class CreateChildProfileDto
    {
        public string Nickname { get; set; } = string.Empty;

        public int AgeInYears { get; set; }

        public int AgeInMonths { get; set; }

        public Gender Gender { get; set; } = Gender.PreferNotToSay;

        public SupportNeedsLevel SupportNeedsLevel { get; set; } = SupportNeedsLevel.Medium;

        public string? MainDailyChallengesJson { get; set; }

        public string? StrengthsAndInterests { get; set; }

        public bool PrefersVisualSchedules { get; set; } = false;

        public string? CommunicationMethodsJson { get; set; }
    }
}