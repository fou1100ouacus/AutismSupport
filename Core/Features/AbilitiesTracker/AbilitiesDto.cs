namespace Core.AbilitiesTracker
{
    public class SubmitTestDto
    {
        public int ChildId { get; set; }
        public int CategoryId { get; set; }
        // القائمة تحتوي على (معرف السؤال : القيمة المختارة 0-4)
        public Dictionary<int, int> Answers { get; set; } = new();
    }


    public class AbilityTestResultDto
    {
        public int Id { get; set; }
        public int TotalScore { get; set; }
        public string Level { get; set; } = string.Empty;
        public DateTime TestDate { get; set; }
        public string CategoryNameAr { get; set; } = string.Empty;
        public string CategoryNameEn { get; set; } = string.Empty;
    }
    
    
    }