using Data.Entities.Community;
using Data.Enums;
using Infrastructure.Repositories;
using Service.Abstracts;

namespace Service.Implementations
{
    public static class KeywordBlocklist
    {
        private static readonly List<string> ForbiddenWords = new()
        {
            "diagnos", "medication", "therapy", "treatment", "prescription",
            "doctor", "medical", "symptom", "disorder", "cure", "drug",
            "تشخيص", "دواء", "علاج", "طبيب", "عقار", "أعراض"
        };

        public static bool ContainsForbiddenWords(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            var lower = text.ToLower();
            return ForbiddenWords.Any(w => lower.Contains(w));
        }
    }

    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository _postRepo;
        public CommunityPostService(ICommunityPostRepository postRepo) => _postRepo = postRepo;

        public async Task<CommunityPost> GetByIdAsync(int id) => await _postRepo.GetByIdAsync(id);

        public async Task<(List<CommunityPost> Items, int TotalCount)> GetFeedAsync(int pageNumber, int pageSize) =>
            await _postRepo.GetApprovedFeedAsync(pageNumber, pageSize);

        public async Task<CommunityPost> CreateAsync(string content, string photoUrl, int userId)
        {
            if (KeywordBlocklist.ContainsForbiddenWords(content))
                throw new InvalidOperationException("ContentContainsForbiddenMedicalTerms");

            var post = new CommunityPost
            {
                UserId = userId,
                Content = content,
                PhotoUrl = photoUrl,
                Status = PostStatus.Pending
            };
            return await _postRepo.AddAsync(post);
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var post = await _postRepo.GetByIdAsync(id);
            if (post == null || post.UserId != userId)
                throw new UnauthorizedAccessException("NotAuthorized");
            return await _postRepo.SoftDeleteAsync(id);
        }

        public async Task ModerateAsync(int id, PostStatus status, string note, int moderatorId) =>
            await _postRepo.UpdateStatusAsync(id, status, note, moderatorId);
    }

    public class CommunityCommentService : ICommunityCommentService
    {
        private readonly ICommunityCommentRepository _commentRepo;
        private readonly ICommunityPostRepository _postRepo;

        public CommunityCommentService(ICommunityCommentRepository commentRepo, ICommunityPostRepository postRepo)
        {
            _commentRepo = commentRepo;
            _postRepo = postRepo;
        }

        public async Task<List<CommunityComment>> GetByPostIdAsync(int postId) =>
            await _commentRepo.GetApprovedByPostIdAsync(postId);

        public async Task<CommunityComment> CreateAsync(int postId, string content, int userId)
        {
            var post = await _postRepo.GetByIdAsync(postId);
            if (post == null || post.Status != PostStatus.Approved)
                throw new InvalidOperationException("PostNotFoundOrNotApproved");

            if (KeywordBlocklist.ContainsForbiddenWords(content))
                throw new InvalidOperationException("ContentContainsForbiddenMedicalTerms");

            var comment = new CommunityComment
            {
                PostId = postId,
                UserId = userId,
                Content = content,
                Status = CommentStatus.Pending
            };

            await _commentRepo.AddAsync(comment);

            // زودي عداد التعليقات في البوست
            post.CommentsCount++;
            await _postRepo.UpdateAsync(post);

            return comment;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null || comment.UserId != userId)
                throw new UnauthorizedAccessException("NotAuthorized");

            // طرح عداد التعليقات من البوست
            var post = await _postRepo.GetByIdAsync(comment.PostId);
            if (post != null)
            {
                post.CommentsCount = Math.Max(0, post.CommentsCount - 1);
                await _postRepo.UpdateAsync(post);
            }

            return await _commentRepo.SoftDeleteAsync(id);
        }

        public async Task ModerateAsync(int id, CommentStatus status, string note, int moderatorId) =>
            await _commentRepo.UpdateStatusAsync(id, status, note, moderatorId);
    }

    public class CommunityReactionService : ICommunityReactionService
    {
        private readonly ICommunityReactionRepository _reactionRepo;
        private readonly ICommunityPostRepository _postRepo;
        private readonly ICommunityCommentRepository _commentRepo;

        public CommunityReactionService(
            ICommunityReactionRepository reactionRepo,
            ICommunityPostRepository postRepo,
            ICommunityCommentRepository commentRepo)
        {
            _reactionRepo = reactionRepo;
            _postRepo = postRepo;
            _commentRepo = commentRepo;
        }

        public async Task ToggleReactionAsync(int userId, ReactionTargetType targetType, int targetId, ReactionType reactionType)
        {
            var existing = await _reactionRepo.GetUserReactionAsync(userId, targetType, targetId);
            if (existing != null)
            {
                // امسح الـ Reaction
                await _reactionRepo.DeleteAsync(existing.Id);

                // طرح العداد
                if (targetType == ReactionTargetType.Post)
                {
                    var post = await _postRepo.GetByIdAsync(targetId);
                    if (post != null)
                    {
                        post.ReactionsCount = Math.Max(0, post.ReactionsCount - 1);
                        await _postRepo.UpdateAsync(post);
                    }
                }
                else if (targetType == ReactionTargetType.Comment)
                {
                    var comment = await _commentRepo.GetByIdAsync(targetId);
                    if (comment != null)
                    {
                        comment.ReactionsCount = Math.Max(0, comment.ReactionsCount - 1);
                        await _commentRepo.UpdateAsync(comment);
                    }
                }
            }
            else
            {
                // اضيف الـ Reaction
                var reaction = new CommunityReaction
                {
                    UserId = userId,
                    ReactionType = reactionType,
                    TargetType = targetType,
                    PostId = targetType == ReactionTargetType.Post ? targetId : null,
                    CommentId = targetType == ReactionTargetType.Comment ? targetId : null
                };
                await _reactionRepo.AddAsync(reaction);

                // زود العداد
                if (targetType == ReactionTargetType.Post)
                {
                    var post = await _postRepo.GetByIdAsync(targetId);
                    if (post != null)
                    {
                        post.ReactionsCount++;
                        await _postRepo.UpdateAsync(post);
                    }
                }
                else if (targetType == ReactionTargetType.Comment)
                {
                    var comment = await _commentRepo.GetByIdAsync(targetId);
                    if (comment != null)
                    {
                        comment.ReactionsCount++;
                        await _commentRepo.UpdateAsync(comment);
                    }
                }
            }
        }
    }

    public class CommunityReportService : ICommunityReportService
    {
        private readonly ICommunityReportRepository _reportRepo;
        public CommunityReportService(ICommunityReportRepository reportRepo) => _reportRepo = reportRepo;

        public async Task CreateAsync(int userId, ReportTargetType targetType, int targetId, string reason)
        {
            var report = new CommunityReport
            {
                ReportedByUserId = userId,
                TargetType = targetType,
                PostId = targetType == ReportTargetType.Post ? targetId : null,
                CommentId = targetType == ReportTargetType.Comment ? targetId : null,
                Reason = reason,
                Status = ReportStatus.Open
            };
            await _reportRepo.AddAsync(report);
        }

        public async Task<List<CommunityReport>> GetOpenReportsAsync() => await _reportRepo.GetOpenReportsAsync();

        public async Task ResolveAsync(int reportId, string note) =>
            await _reportRepo.UpdateStatusAsync(reportId, ReportStatus.Resolved, note);
    }
}
