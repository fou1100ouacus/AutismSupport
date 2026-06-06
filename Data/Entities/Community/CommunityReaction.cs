using Data.Entities.Identity;
using Data.Enums;

namespace Data.Entities.Community
{
    public class CommunityReaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ReactionType ReactionType { get; set; }
        public ReactionTargetType TargetType { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
        public virtual CommunityPost Post { get; set; }
        public virtual CommunityComment Comment { get; set; }
    }
}

