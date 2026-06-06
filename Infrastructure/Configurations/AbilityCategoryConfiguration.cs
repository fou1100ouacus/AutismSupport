using Data.Entities.AbilitiesTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AbilityCategoryConfiguration : IEntityTypeConfiguration<AbilityCategory>
    {
        public void Configure(EntityTypeBuilder<AbilityCategory> builder)
        {
            builder.ToTable("AbilityCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NameEn)
                   .IsRequired()
                   .HasMaxLength(200);

            // علاقة رأس بأطراف مع الأسئلة
            builder.HasMany(x => x.Questions)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}