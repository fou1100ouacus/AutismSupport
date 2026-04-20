using Data.Entities;
using Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities.Child;
namespace Infrastructure.Configurations
{
    public class ChildConfiguration : IEntityTypeConfiguration<ChildProfile>
    {   
        public void Configure(EntityTypeBuilder<ChildProfile> builder)
        {
            builder.ToTable("ChildProfiles");

            // Primary Key
            builder.HasKey(c => c.Id);

            // One-to-One Relationship with User (Mother)
            builder.HasOne(c => c.Mother)
                   .WithOne(u => u.ChildProfile)
                   .HasForeignKey<ChildProfile>(c => c.MotherId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Unique Constraint: كل أم لها طفل واحد فقط
            builder.HasIndex(c => c.MotherId)
                   .IsUnique();

            // Required Fields
            builder.Property(c => c.Nickname)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.AgeInYears)
                   .IsRequired();

            builder.Property(c => c.AgeInMonths)
                   .IsRequired();

            // Enums Configuration
            builder.Property(c => c.Gender)
                   .HasConversion<int>()           // يخزن الـ enum كـ integer
                   .IsRequired();

            builder.Property(c => c.SupportNeedsLevel)
                   .HasConversion<int>()
                   .IsRequired();

            // JSON Columns Configuration (مهم جداً)
            builder.Property(c => c.MainDailyChallengesJson)
                   .HasColumnType("nvarchar(max)");

            builder.Property(c => c.CommunicationMethodsJson)
                   .HasColumnType("nvarchar(max)");

            builder.Property(c => c.StrengthsAndInterests)
                   .HasColumnType("nvarchar(max)");

            // Boolean
            builder.Property(c => c.PrefersVisualSchedules)
                   .IsRequired()
                   .HasDefaultValue(false);

            // Timestamps
            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            builder.Property(c => c.LastUpdatedAt)
                   .IsRequired();

            // Indexes for better performance
            builder.HasIndex(c => c.CreatedAt);
        }
    }
}