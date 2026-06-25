using System;
using Data.Entities.Identity; // تأكدي من مسار كلاس الـ Child عندك
using Data.Entities.Child; // تأكدي من مسار كلاس الـ Child عندك
namespace Data.Entities.AbilitiesTracker
{
    public class MotionAnalysisResult
    {
        public int Id { get; set; }
        
        // ربط التحليل بالطفل المحدد الذي سيتم جلبه تلقائياً من الـ Token
        public int ChildId { get; set; }
        public virtual ChildProfile Child { get; set; }

        public string VideoUrl { get; set; } // رابط حفظ الفيديو محلياً على السيرفر
        
        // حالات التحليل لمنع الـ Timeout (Pending, Processing, Completed, Failed)
        public string Status { get; set; } = "Pending"; 
        
        public string Prediction { get; set; } // النتيجة النصية النهائية (مثل: Normal Motion Activity)
        public double? SmmPercentage { get; set; } // نسبة الحركات النمطية القادمة من الـ AI
        public int? SmmSegmentsCount { get; set; } // عدد المقاطع المكتشفة
        public int? TotalSegments { get; set; } // إجمالي عدد المقاطع
        public double? VideoDuration { get; set; } // مدة الفيديو بالثواني
        public string SegmentsJson { get; set; } // تخزين المقاطع كـ JSON
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}