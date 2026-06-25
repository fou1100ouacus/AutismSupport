using Data.Entities.AbilitiesTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class MotionAnalysisResultConfiguration : IEntityTypeConfiguration<MotionAnalysisResult>
    {
        public void Configure(EntityTypeBuilder<MotionAnalysisResult> builder)
        {
            // 1️⃣ تحديد اسم الجدول في الـ SQL Server
            builder.ToTable("MotionAnalysisResults");

            // 2️⃣ تحديد المفتاح الأساسي (Primary Key)
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // 3️⃣ إعدادات الحقول والنصوص (Property Configurations)
            builder.Property(x => x.VideoUrl)
                .IsRequired()
                .HasMaxLength(500); // مسار الفيديو

            builder.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Pending"); // الحالة الافتراضية للعملية في الخلفية

            builder.Property(x => x.Prediction)
                .HasMaxLength(250)
                .IsRequired(false); // يمكن أن يكون Null في البداية حتى ينتهي الـ AI

            builder.Property(x => x.SmmPercentage)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)"); // تحديد نوع عشري دقيق للنسبة المئوية لـ SQL Server

            builder.Property(x => x.SmmSegmentsCount)
                .IsRequired(false);

            builder.Property(x => x.TotalSegments)
                .IsRequired(false);

            builder.Property(x => x.VideoDuration)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.SegmentsJson)
                .IsRequired(false)
                .HasMaxLength(4000); // JSON string for segments

            // 4️⃣ 👶 إعداد الـ Foreign Key والعلاقة مع جدول الطفل (Child)
            // العلاقة: الطفل الواحد يمكن أن يكون له عدة تحليلات حركية (One-to-Many)
            builder.HasOne(x => x.Child)
                .WithMany() // إذا كان كلاس الـ Child لا يحتوي على ICollection<MotionAnalysisResult>
                .HasForeignKey(x => x.ChildId)
                .OnDelete(DeleteBehavior.Cascade); // إذا حُذف بروفايل الطفل، تُحذف تحليلاته تلقائياً
        }
    }
}