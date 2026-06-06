using Core.Bases;
using Core.Features.Community.Moderation.Queries.Results;
using Infrastructure.Repositories;
using MediatR;

namespace Core.Features.Community.Moderation.Queries.Models
{
    public class GetModerationQueueQuery : IRequest<Response<ModerationQueueResult>> { }
}

namespace Core.Features.Community.Moderation.Queries.Results
{
    public class ModerationQueueResult
    {
        public List<PendingPostItem> PendingPosts { get; set; } = new();
        public List<PendingCommentItem> PendingComments { get; set; } = new();
        public List<OpenReportItem> OpenReports { get; set; } = new();
    }

    public class PendingPostItem
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string CreatedAt { get; set; }
    }

    public class PendingCommentItem
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string CreatedAt { get; set; }
    }

    public class OpenReportItem
    {
        public int Id { get; set; }
        public string TargetType { get; set; }
        public int TargetId { get; set; }
        public string Reason { get; set; }
        public string ReportedBy { get; set; }
        public string CreatedAt { get; set; }
    }
}

namespace Core.Features.Community.Moderation.Queries.Handlers
{
    using Core.Features.Community.Moderation.Queries.Models;
    using Core.Features.Community.Moderation.Queries.Results;

    public class ModerationQueryHandler : IRequestHandler<GetModerationQueueQuery, Response<ModerationQueueResult>>
    {
        private readonly ICommunityPostRepository _postRepo;
        private readonly ICommunityCommentRepository _commentRepo;
        private readonly ICommunityReportRepository _reportRepo;
        private readonly ResponseHandler _responseHandler;

        public ModerationQueryHandler(
            ICommunityPostRepository postRepo,
            ICommunityCommentRepository commentRepo,
            ICommunityReportRepository reportRepo,
            ResponseHandler responseHandler)
        {
            _postRepo = postRepo;
            _commentRepo = commentRepo;
            _reportRepo = reportRepo;
            _responseHandler = responseHandler;
        }

        public async Task<Response<ModerationQueueResult>> Handle(GetModerationQueueQuery request, CancellationToken cancellationToken)
        {
            var pendingPosts = await _postRepo.GetPendingAsync();
            var pendingComments = await _commentRepo.GetPendingAsync();
            var openReports = await _reportRepo.GetOpenReportsAsync();

            var result = new ModerationQueueResult
            {
                PendingPosts = pendingPosts.Select(p => new PendingPostItem
                {
                    Id = p.Id,
                    AuthorName = p.User?.UserName ?? "Unknown",
                    Content = p.Content,
                    PhotoUrl = p.PhotoUrl,
                    CreatedAt = p.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList(),

                PendingComments = pendingComments.Select(c => new PendingCommentItem
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    AuthorName = c.User?.UserName ?? "Unknown",
                    Content = c.Content,
                    CreatedAt = c.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList(),

                OpenReports = openReports.Select(r => new OpenReportItem
                {
                    Id = r.Id,
                    TargetType = r.TargetType.ToString(),
                    TargetId = r.PostId ?? r.CommentId ?? 0,
                    Reason = r.Reason,
                    ReportedBy = r.ReportedByUser?.UserName ?? "Unknown",
                    CreatedAt = r.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList()
            };

            return _responseHandler.Success(result);
        }
    }
}
