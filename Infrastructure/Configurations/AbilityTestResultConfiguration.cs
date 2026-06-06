using Data.Entities.AbilitiesTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AbilityTestResultConfiguration : IEntityTypeConfiguration<AbilityTestResult>
    {
        public void Configure(EntityTypeBuilder<AbilityTestResult> builder)
        {
            builder.ToTable("AbilityTestResults");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Level)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.DetailedAnswersJson)
                   .IsRequired(false);

            // ربط النتيجة بالطفل (علاقة أطراف برأس)
            builder.HasOne(x => x.Child)
                   .WithMany(c => c.AbilityTestResults)
                   .HasForeignKey(x => x.ChildId)
                   .OnDelete(DeleteBehavior.Cascade); // إذا حُذف الطفل تُحذف نتائجه

            // ربط النتيجة بالقسم
            builder.HasOne(x => x.Category)
                   .WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}