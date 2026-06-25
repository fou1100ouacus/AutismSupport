namespace Data.DTOs
{
    public class AiAnalysisResponse
    {
        public AiReport Report { get; set; }
        public List<AiSegment> Segments { get; set; }
    }

    public class AiReport
    {
        public double SPercentage { get; set; }
        public int SSegmentsCount { get; set; }
        public int TotalSegments { get; set; }
        public double VideoDurationSeconds { get; set; }
    }

    public class AiSegment
    {
        public double EndTime { get; set; }
        public bool IsS { get; set; }
        public double SScore { get; set; }
        public double StartTime { get; set; }
    }
}
