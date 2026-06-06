using Data.Entities.Community;
using Data.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CommunityPostRepository : ICommunityPostRepository
    {
        private readonly ApplicationDBContext _context;
        public CommunityPostRepository(ApplicationDBContext context) => _context = context;

        public async Task<CommunityPost> GetByIdAsync(int id) =>
            await _context.CommunityPosts
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<(List<CommunityPost> Items, int TotalCount)> GetApprovedFeedAsync(int pageNumber, int pageSize)
        {
            var query = _context.CommunityPosts
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Where(x => x.Status == PostStatus.Approved && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt);

            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<List<CommunityPost>> GetPendingAsync() =>
            await _context.CommunityPosts
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Where(x => x.Status == PostStatus.Pending && !x.IsDeleted)
                .OrderBy(x => x.CreatedAt).ToListAsync();

        public async Task<CommunityPost> AddAsync(CommunityPost post)
        {
            await _context.CommunityPosts.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<CommunityPost> UpdateAsync(CommunityPost post)
        {
            post.UpdatedAt = DateTime.UtcNow;
            _context.CommunityPosts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var post = await _context.CommunityPosts.FindAsync(id);
            if (post == null) return false;
            post.IsDeleted = true;
            post.Status = PostStatus.Deleted;
            post.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateStatusAsync(int id, PostStatus status, string note, int moderatorId)
        {
            var post = await _context.CommunityPosts.FindAsync(id);
            if (post == null) return;
            post.Status = status;
            post.ModerationNote = note;
            post.ModeratedByUserId = moderatorId;
            post.ModeratedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public class CommunityCommentRepository : ICommunityCommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommunityCommentRepository(ApplicationDBContext context) => _context = context;

        public async Task<CommunityComment> GetByIdAsync(int id) =>
            await _context.CommunityComments
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<List<CommunityComment>> GetApprovedByPostIdAsync(int postId) =>
            await _context.CommunityComments
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Where(x => x.PostId == postId && x.Status == CommentStatus.Approved && !x.IsDeleted)
                .OrderBy(x => x.CreatedAt).ToListAsync();

        public async Task<List<CommunityComment>> GetPendingAsync() =>
            await _context.CommunityComments
                .IgnoreQueryFilters()
                .Include(x => x.User)
                .Where(x => x.Status == CommentStatus.Pending && !x.IsDeleted)
                .OrderBy(x => x.CreatedAt).ToListAsync();

        public async Task<CommunityComment> AddAsync(CommunityComment comment)
        {
            await _context.CommunityComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<CommunityComment> UpdateAsync(CommunityComment comment)
        {
            comment.UpdatedAt = DateTime.UtcNow;
            _context.CommunityComments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var comment = await _context.CommunityComments.FindAsync(id);
            if (comment == null) return false;
            comment.IsDeleted = true;
            comment.Status = CommentStatus.Deleted;
            comment.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateStatusAsync(int id, CommentStatus status, string note, int moderatorId)
        {
            var comment = await _context.CommunityComments.FindAsync(id);
            if (comment == null) return;
            comment.Status = status;
            comment.ModerationNote = note;
            comment.ModeratedByUserId = moderatorId;
            comment.ModeratedAt = DateTime.UtcNow;
            comment.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public class CommunityReactionRepository : ICommunityReactionRepository
    {
        private readonly ApplicationDBContext _context;
        public CommunityReactionRepository(ApplicationDBContext context) => _context = context;

        public async Task<CommunityReaction> GetUserReactionAsync(int userId, ReactionTargetType targetType, int targetId) =>
            targetType == ReactionTargetType.Post
                ? await _context.CommunityReactions.FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == targetId)
                : await _context.CommunityReactions.FirstOrDefaultAsync(x => x.UserId == userId && x.CommentId == targetId);

        public async Task<CommunityReaction> AddAsync(CommunityReaction reaction)
        {
            await _context.CommunityReactions.AddAsync(reaction);
            await _context.SaveChangesAsync();
            return reaction;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reaction = await _context.CommunityReactions.FindAsync(id);
            if (reaction == null) return false;
            _context.CommunityReactions.Remove(reaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class CommunityReportRepository : ICommunityReportRepository
    {
        private readonly ApplicationDBContext _context;
        public CommunityReportRepository(ApplicationDBContext context) => _context = context;

        public async Task<CommunityReport> AddAsync(CommunityReport report)
        {
            await _context.CommunityReports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<CommunityReport>> GetOpenReportsAsync() =>
            await _context.CommunityReports
                .Include(x => x.ReportedByUser)
                .Where(x => x.Status == ReportStatus.Open)
                .OrderBy(x => x.CreatedAt).ToListAsync();

        public async Task UpdateStatusAsync(int id, ReportStatus status, string note)
        {
            var report = await _context.CommunityReports.FindAsync(id);
            if (report == null) return;
            report.Status = status;
            report.ResolutionNote = note;
            report.ResolvedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
