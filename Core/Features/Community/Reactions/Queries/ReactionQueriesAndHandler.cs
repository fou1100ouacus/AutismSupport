using Core.Bases;
using Data.Enums;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;

namespace Core.Features.Community.Reactions.Queries.Models
{
    public class GetReactionsCountQuery : IRequest<Response<int>>
    {
        public int PostId { get; set; }
    }

    public class GetMyReactionQuery : IRequest<Response<bool>>
    {
        public int PostId { get; set; }
    }
}

namespace Core.Features.Community.Reactions.Queries.Handlers
{
    using Core.Features.Community.Reactions.Queries.Models;

    public class ReactionQueryHandler :
        IRequestHandler<GetReactionsCountQuery, Response<int>>,
        IRequestHandler<GetMyReactionQuery, Response<bool>>
    {
        private readonly ICommunityPostService _postService;
        private readonly ICommunityReactionService _reactionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ResponseHandler _responseHandler;

        public ReactionQueryHandler(
            ICommunityPostService postService,
            ICommunityReactionService reactionService,
            ICurrentUserService currentUserService,
            ResponseHandler responseHandler)
        {
            _postService = postService;
            _reactionService = reactionService;
            _currentUserService = currentUserService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<int>> Handle(GetReactionsCountQuery request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdAsync(request.PostId);
            if (post == null)
                return _responseHandler.NotFound<int>("البوست مش موجود");

            return _responseHandler.Success(post.ReactionsCount);
        }

        public async Task<Response<bool>> Handle(GetMyReactionQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            var hasReacted = await _reactionService.HasUserReactedAsync(userId, ReactionTargetType.Post, request.PostId);
            return _responseHandler.Success(hasReacted);
        }
    }
}