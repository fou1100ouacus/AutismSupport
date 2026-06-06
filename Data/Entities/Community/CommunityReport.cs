using Data.Entities.Identity;
using Data.Enums;

namespace Data.Entities.Community
{
    public class CommunityReport
    {
        public int Id { get; set; }
        public int ReportedByUserId { get; set; }
        public ReportTargetType TargetType { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string Reason { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Open;
        public int? AssignedToModeratorId { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? ResolutionNote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User ReportedByUser { get; set; }
        public virtual CommunityPost Post { get; set; }
        public virtual CommunityComment Comment { get; set; }
    }
}
