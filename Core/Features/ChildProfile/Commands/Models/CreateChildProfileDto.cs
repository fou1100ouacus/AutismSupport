// using Data.Entities.Enums;

// namespace Core.Features.ChildProfile.Models
// {
//     public class CreateChildProfileDto
//     {
//         public string Nickname { get; set; } = string.Empty;

//         public int AgeInYears { get; set; }

//         public int AgeInMonths { get; set; }

//         public Gender Gender { get; set; } = Gender.PreferNotToSay;

//         public SupportNeedsLevel SupportNeedsLevel { get; set; } = SupportNeedsLevel.Medium;

//         public string? MainDailyChallengesJson { get; set; }

//         public string? StrengthsAndInterests { get; set; }

//         public bool PrefersVisualSchedules { get; set; } = false;

//         public string? CommunicationMethodsJson { get; set; }
//     }
// }


using Data.Entities.Enums;
using System.Collections.Generic; //

namespace Core.Features.ChildProfile.Models
{
    public class CreateChildProfileDto
    {
        public string Nickname { get; set; } = string.Empty;

        public int AgeInYears { get; set; }

        public int AgeInMonths { get; set; }
               /// <summary>
               ///  Male = 1,
              ///Female = 2,
              /// Other = 3,
                /// PreferNotToSay = 4
               /// Gender
               /// </summary>
            
        public Gender Gender { get; set; } = Gender.PreferNotToSay;

        /// <summary>
        /// Level of support required for the child:
        /// 1 = Light
        /// 2 = Medium
        /// 3 = High
        /// </summary>
        
        public SupportNeedsLevel SupportNeedsLevel { get; set; } = SupportNeedsLevel.Medium;

        public bool PrefersVisualSchedules { get; set; } = false;

        // المواضيع المختارة تحولت بالكامل إلى قوائم لربطها بالـ Chips
        public List<string> MainDailyChallenges { get; set; } = new List<string>();

        public List<string> CommunicationMethods { get; set; } = new List<string>();

        public List<string> StrengthsAndInterests { get; set; } = new List<string>();
    }
}