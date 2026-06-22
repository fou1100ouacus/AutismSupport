using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Core.Features.AbilitiesTracker.MotionAnalysis.Commands.Models
{
    // 1. الـ Request القادم من الـ Controller (يستقبل الفيديو فقط من الفرونت إند)
    public class AnalyzeChildMotionCommand : IRequest<int>
    {
        public IFormFile VideoFile { get; set; }
        
        // سيتم حقنه داخلياً من الـ Token لحماية البيانات ومنع التلاعب
        public int AutomatedChildId { get; set; }
    }

    // 2. الموديلات المطابقة تماماً للـ JSON الراجع من خادم الـ AI على Hugging Face
    public class HuggingFaceAiResponse
    {
        public AiReportDto Report { get; set; }
        public List<AiSegmentDto> Segments { get; set; }
    }

    public class AiReportDto
    {
        public double Smm_Percentage { get; set; }
        public int Smm_Segments_Count { get; set; }
        public int Total_Segments { get; set; }
        public double Video_Duration_Seconds { get; set; }
    }

    public class AiSegmentDto
    {
        public double Start_Time { get; set; }
        public double End_Time { get; set; }
        public bool Is_Smm { get; set; }
        public double Smm_Score { get; set; }
    }
}