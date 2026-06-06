using Data.Entities.Identity;
using Data.Enums;

namespace Data.Entities.Community
{
    public class CommunityComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public CommentStatus Status { get; set; } = CommentStatus.Pending;
        public string? ModerationNote { get; set; }
        public int? ModeratedByUserId { get; set; }
        public DateTime? ModeratedAt { get; set; }
        public int ReactionsCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual CommunityPost Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommunityReaction> Reactions { get; set; }
    }
}
