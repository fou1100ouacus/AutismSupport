using Data.Entities.Community;
using Data.Enums;

namespace Service.Abstracts
{
    public interface ICommunityPostService
    {
        Task<CommunityPost> GetByIdAsync(int id);
        Task<(List<CommunityPost> Items, int TotalCount)> GetFeedAsync(int pageNumber, int pageSize);
        Task<CommunityPost> CreateAsync(string content, string? photoUrl, int userId);
        Task<bool> DeleteAsync(int id, int userId);
        Task ModerateAsync(int id, PostStatus status, string note, int moderatorId);
    }

    public interface ICommunityCommentService
    {
        Task<List<CommunityComment>> GetByPostIdAsync(int postId);
        Task<CommunityComment> CreateAsync(int postId, string content, int userId);
        Task<bool> DeleteAsync(int id, int userId);
        Task ModerateAsync(int id, CommentStatus status, string note, int moderatorId);
    }

    public interface ICommunityReactionService
    {
        Task ToggleReactionAsync(int userId, ReactionTargetType targetType, int targetId, ReactionType reactionType);
        Task<bool> HasUserReactedAsync(int userId, ReactionTargetType targetType, int targetId);
    }

    public interface ICommunityReportService
    {
        Task CreateAsync(int userId, ReportTargetType targetType, int targetId, string reason);
        Task<List<CommunityReport>> GetOpenReportsAsync();
        Task ResolveAsync(int reportId, string note);
    }
}