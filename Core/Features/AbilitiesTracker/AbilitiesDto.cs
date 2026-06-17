// namespace Core.AbilitiesTracker
// {
//     public class SubmitTestDto
//     {
//         public int ChildId { get; set; }
//         public int CategoryId { get; set; }
//         // القائمة تحتوي على (معرف السؤال : القيمة المختارة 0-4)
//         public Dictionary<int, int> Answers { get; set; } = new();
//     }


//     public class AbilityTestResultDto
//     {
//         public int Id { get; set; }
//         public int TotalScore { get; set; }
//         public string Level { get; set; } = string.Empty;
//         public DateTime TestDate { get; set; }
//         public string CategoryNameAr { get; set; } = string.Empty;
//         public string CategoryNameEn { get; set; } = string.Empty;
//     }
    
    
//     }

using System;
using System.Collections.Generic;

namespace Core.Features.AbilitiesTracker
{
    // الكلاس القديم تم الحفاظ عليه لمنع أي كسر في ملفات أخرى
    public class AbilitiesDto
    {
        public class SubmitTestDto
        {
            public int ChildId { get; set; }
            public int CategoryId { get; set; }
            public Dictionary<int, int> Answers { get; set; } = new();
        }

        public class AbilityTestResultDto
        {
            public int Id { get; set; }
            public int TotalScore { get; set; }
            public string Level { get; set; } = string.Empty;
            public DateTime TestDate { get; set; }
      //      public string CategoryNameAr { get; set; } = string.Empty;
            public string CategoryNameEn { get; set; } = string.Empty;
        }
    }

    // =======================================================
    // الـ DTOs الجديدة مفرودة ومستقلة هنا لتجنب الـ Circular Dependency والـ Inaccessibility
    // =======================================================
    
    public class SubmitTestRequestDto
    {
        public List<QuestionAnswerDto> Answers { get; set; } = new();
    }

    public class QuestionAnswerDto
    {
       public int QuestionId { get; set; }

        /// <summary>
        /// Answer value for the question:
        /// 0 = Never
        /// 1 = Sometimes
        /// 2 = Always
        /// </summary>
        public int AnswerValue { get; set; }
    }

    public class TestResultResponseDto
    {
        public string RiskLevel { get; set; } = string.Empty; // Low, Medium, High
        public double TotalPercentage { get; set; }
        public List<CategoryScoreDto> ObservedBehaviors { get; set; } = new();
        public List<string> Recommendations { get; set; } = new();
    }

    public class CategoryResultDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryScore { get; set; }
        public string Status { get; set; } = string.Empty; // Good, Average, Needs Support
    }

    //   public class TestResultResponseDto
    // {
    //     public string RiskLevel { get; set; } = string.Empty; // Low, Medium, High
    //     public double TotalPercentage { get; set; }
    //     public List<CategoryScoreDto> ObservedBehaviors { get; set; } = new();
    // }

    public class CategoryScoreDto
    {
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryScore { get; set; }
        public string Status { get; set; } = string.Empty; // Needs Support, Typical, etc.
    }
}