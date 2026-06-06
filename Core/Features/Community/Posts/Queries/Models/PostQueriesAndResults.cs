using Core.Bases;
using Core.Features.Community.Posts.Queries.Results;
using MediatR;

namespace Core.Features.Community.Posts.Queries.Models
{
    public class GetPostFeedQuery : IRequest<Response<GetPostFeedResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPostByIdQuery : IRequest<Response<GetPostResult>>
    {
        public int Id { get; set; }
        public GetPostByIdQuery(int id) => Id = id;
    }
}

namespace Core.Features.Community.Posts.Queries.Results
{
    public class GetPostResult
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string Status { get; set; }
        public int ReactionsCount { get; set; }
        public int CommentsCount { get; set; }
        public string CreatedAt { get; set; }
    }

    public class GetPostFeedResult
    {
        public List<GetPostResult> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

