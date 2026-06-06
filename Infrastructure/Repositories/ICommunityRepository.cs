using Data.Entities.Community;
using Data.Enums;

namespace Infrastructure.Repositories
{
    public interface ICommunityPostRepository
    {
        Task<CommunityPost> GetByIdAsync(int id);
        Task<(List<CommunityPost> Items, int TotalCount)> GetApprovedFeedAsync(int pageNumber, int pageSize);
        Task<List<CommunityPost>> GetPendingAsync();
        Task<CommunityPost> AddAsync(CommunityPost post);
        Task<CommunityPost> UpdateAsync(CommunityPost post);
        Task<bool> SoftDeleteAsync(int id);
        Task UpdateStatusAsync(int id, PostStatus status, string note, int moderatorId);
    }

    public interface ICommunityCommentRepository
    {
        Task<CommunityComment> GetByIdAsync(int id);
        Task<List<CommunityComment>> GetApprovedByPostIdAsync(int postId);
        Task<List<CommunityComment>> GetPendingAsync();
        Task<CommunityComment> AddAsync(CommunityComment comment);
        Task<CommunityComment> UpdateAsync(CommunityComment comment);
        Task<bool> SoftDeleteAsync(int id);
        Task UpdateStatusAsync(int id, CommentStatus status, string note, int moderatorId);
    }

    public interface ICommunityReactionRepository
    {
        Task<CommunityReaction> GetUserReactionAsync(int userId, ReactionTargetType targetType, int targetId);
        Task<CommunityReaction> AddAsync(CommunityReaction reaction);
        Task<bool> DeleteAsync(int id);
    }

    public interface ICommunityReportRepository
    {
        Task<CommunityReport> AddAsync(CommunityReport report);
        Task<List<CommunityReport>> GetOpenReportsAsync();
        Task UpdateStatusAsync(int id, ReportStatus status, string note);
    }
}
