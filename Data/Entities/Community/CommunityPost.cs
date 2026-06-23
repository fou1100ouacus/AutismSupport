using Data.Entities.Identity;
using Data.Enums;

namespace Data.Entities.Community
{
    public class CommunityPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public PostStatus Status { get; set; } = PostStatus.Pending;
        public string? ModerationNote { get; set; }
        public int? ModeratedByUserId { get; set; }
        public DateTime? ModeratedAt { get; set; }
        public int ReactionsCount { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual User User { get; set; }
        public virtual ICollection<CommunityComment> Comments { get; set; }
        public virtual ICollection<CommunityReaction> Reactions { get; set; }
        public virtual ICollection<CommunityReport> Reports { get; set; }
    }
}
