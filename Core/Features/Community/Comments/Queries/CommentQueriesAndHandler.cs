using Core.Bases;
using MediatR;
using Service.Abstracts;

namespace Core.Features.Community.Comments.Queries.Models
{
    public class GetCommentsByPostIdQuery : IRequest<Response<List<Core.Features.Community.Comments.Queries.Results.GetCommentResult>>>
    {
        public int PostId { get; set; }
        public GetCommentsByPostIdQuery(int postId) => PostId = postId;
    }
}

namespace Core.Features.Community.Comments.Queries.Results
{
    public class GetCommentResult
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public int ReactionsCount { get; set; }
        public string CreatedAt { get; set; }
    }
}

namespace Core.Features.Community.Comments.Queries.Handlers
{
    using Core.Features.Community.Comments.Queries.Models;
    using Core.Features.Community.Comments.Queries.Results;

    public class CommentQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, Response<List<GetCommentResult>>>
    {
        private readonly ICommunityCommentService _commentService;
        private readonly ResponseHandler _responseHandler;

        public CommentQueryHandler(ICommunityCommentService commentService, ResponseHandler responseHandler)
        {
            _commentService = commentService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<List<GetCommentResult>>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentService.GetByPostIdAsync(request.PostId);

            var result = comments.Select(c => new GetCommentResult
            {
                Id = c.Id,
                PostId = c.PostId,
                AuthorName = c.User?.UserName ?? "Anonymous",
                Content = c.Content,
                Status = c.Status.ToString(),
                ReactionsCount = c.ReactionsCount,
                CreatedAt = c.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return _responseHandler.Success(result);
        }
    }
}
