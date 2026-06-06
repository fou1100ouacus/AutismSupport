using Data.Entities.AbilitiesTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AbilityQuestionConfiguration : IEntityTypeConfiguration<AbilityQuestion>
    {
        public void Configure(EntityTypeBuilder<AbilityQuestion> builder)
        {
            builder.ToTable("AbilityQuestions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuestionTextAr)
                   .IsRequired();

            builder.Property(x => x.QuestionTextEn)
                   .IsRequired();
        }
    }
}