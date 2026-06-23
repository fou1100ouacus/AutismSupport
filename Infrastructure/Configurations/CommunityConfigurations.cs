using Data.Entities.Community;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CommunityPostConfiguration : IEntityTypeConfiguration<CommunityPost>
    {
        public void Configure(EntityTypeBuilder<CommunityPost> builder)
        {
            builder.ToTable("CommunityPosts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.PhotoUrl).HasMaxLength(500);
            builder.Property(x => x.ModerationNote).HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(PostStatus.Pending);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CommunityCommentConfiguration : IEntityTypeConfiguration<CommunityComment>
    {
        public void Configure(EntityTypeBuilder<CommunityComment> builder)
        {
            builder.ToTable("CommunityComments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.ModerationNote).HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(CommentStatus.Pending);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Post)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(x => x.PostId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CommunityReactionConfiguration : IEntityTypeConfiguration<CommunityReaction>
    {
        public void Configure(EntityTypeBuilder<CommunityReaction> builder)
        {
            builder.ToTable("CommunityReactions");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.UserId, x.PostId, x.TargetType })
                 .IsUnique()
                 .HasFilter("[PostId] IS NOT NULL");


            builder.HasOne(x => x.Comment)
               .WithMany(c => c.Reactions)
               .HasForeignKey(x => x.CommentId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Post)
                   .WithMany(p => p.Reactions)
                   .HasForeignKey(x => x.PostId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CommunityReportConfiguration : IEntityTypeConfiguration<CommunityReport>
    {
        public void Configure(EntityTypeBuilder<CommunityReport> builder)
        {
            builder.ToTable("CommunityReports");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Reason).HasMaxLength(500);
            builder.Property(x => x.ResolutionNote).HasMaxLength(500);
            builder.Property(x => x.Status).HasDefaultValue(ReportStatus.Open);

            builder.HasOne(x => x.Post)
                   .WithMany(p => p.Reports)
                   .HasForeignKey(x => x.PostId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
