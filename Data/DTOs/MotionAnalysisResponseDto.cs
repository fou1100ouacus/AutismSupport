using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class MotionAnalysisResponseDto
    {
        public ReportDto Report { get; set; }
        public List<SegmentDto> Segments { get; set; }
    }

    public class ReportDto
    {
        [JsonPropertyName("smm_percentage")]
        public double SmmPercentage { get; set; }
        
        [JsonPropertyName("smm_segments_count")]
        public int SmmSegmentsCount { get; set; }
        
        [JsonPropertyName("total_segments")]
        public int TotalSegments { get; set; }
        
        [JsonPropertyName("video_duration_seconds")]
        public double VideoDurationSeconds { get; set; }
    }

    public class SegmentDto
    {
        [JsonPropertyName("start_time")]
        public double StartTime { get; set; }
        
        [JsonPropertyName("end_time")]
        public double EndTime { get; set; }
        
        [JsonPropertyName("is_smm")]
        public bool IsSmm { get; set; }
        
        [JsonPropertyName("smm_score")]
        public double SmmScore { get; set; }
    }
}
