using Core.Bases;
using Data.Enums;
using FluentValidation;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;

namespace Core.Features.Community.Comments.Commands.Models
{
    public class CreateCommentCommand : IRequest<Response<string>>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
    }

    public class DeleteCommentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteCommentCommand(int id) => Id = id;
    }

    public class ModerateCommentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public CommentStatus Status { get; set; }
        public string Note { get; set; }
    }
}

namespace Core.Features.Community.Comments.Commands.Validators
{
    using Core.Features.Community.Comments.Commands.Models;

    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.PostId).GreaterThan(0).WithMessage("Id البوست غير صحيح");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("محتوى التعليق مطلوب")
                .MaximumLength(1000).WithMessage("التعليق يجب ألا يتجاوز 1000 حرف");
        }
    }
}

namespace Core.Features.Community.Comments.Commands.Handlers
{
    using Core.Features.Community.Comments.Commands.Models;

    public class CommentCommandHandler :
        IRequestHandler<CreateCommentCommand, Response<string>>,
        IRequestHandler<DeleteCommentCommand, Response<string>>,
        IRequestHandler<ModerateCommentCommand, Response<string>>
    {
        private readonly ICommunityCommentService _commentService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ResponseHandler _responseHandler;

        public CommentCommandHandler(
            ICommunityCommentService commentService,
            ICurrentUserService currentUserService,
            ResponseHandler responseHandler)
        {
            _commentService = commentService;
            _currentUserService = currentUserService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                await _commentService.CreateAsync(request.PostId, request.Content, userId);
                return _responseHandler.Created<string>("تم إرسال التعليق للمراجعة");
            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                await _commentService.DeleteAsync(request.Id, userId);
                return _responseHandler.Deleted<string>("تم الحذف بنجاح");
            }
            catch (UnauthorizedAccessException)
            {
                return _responseHandler.Unauthorized<string>();
            }
        }

        public async Task<Response<string>> Handle(ModerateCommentCommand request, CancellationToken cancellationToken)
        {
            var moderatorId = _currentUserService.GetUserId();
            await _commentService.ModerateAsync(request.Id, request.Status, request.Note, moderatorId);
            return _responseHandler.Success<string>("تم الاعتماد بنجاح");
        }
    }
}
