using Core.Bases;
using Core.Features.Community.Posts.Queries.Models;
using Core.Features.Community.Posts.Queries.Results;
using MediatR;
using Service.Abstracts;

namespace Core.Features.Community.Posts.Queries.Handlers
{
    public class PostQueryHandler :
        IRequestHandler<GetPostFeedQuery, Response<GetPostFeedResult>>,
        IRequestHandler<GetPostByIdQuery, Response<GetPostResult>>
    {
        private readonly ICommunityPostService _postService;
        private readonly ResponseHandler _responseHandler;

        public PostQueryHandler(ICommunityPostService postService, ResponseHandler responseHandler)
        {
            _postService = postService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<GetPostFeedResult>> Handle(GetPostFeedQuery request, CancellationToken cancellationToken)
        {
            var (items, total) = await _postService.GetFeedAsync(request.PageNumber, request.PageSize);
            var result = new GetPostFeedResult
            {
                Items = items.Select(p => new GetPostResult
                {
                    Id = p.Id,
                    AuthorName = p.User?.UserName ?? "Anonymous",
                    Content = p.Content,
                    PhotoUrl = p.PhotoUrl,
                    Status = p.Status.ToString(),
                    ReactionsCount = p.ReactionsCount,
                    CommentsCount = p.CommentsCount,
                    CreatedAt = p.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList(),
                TotalCount = total,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            return _responseHandler.Success(result);
        }

        public async Task<Response<GetPostResult>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(request.Id);
            if (post == null)
                return _responseHandler.NotFound<GetPostResult>("البوست مش موجود");

            return _responseHandler.Success(new GetPostResult
            {
                Id = post.Id,
                AuthorName = post.User?.UserName ?? "Anonymous",
                Content = post.Content,
                PhotoUrl = post.PhotoUrl,
                Status = post.Status.ToString(),
                ReactionsCount = post.ReactionsCount,
                CommentsCount = post.CommentsCount,
                CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }
    }
}
