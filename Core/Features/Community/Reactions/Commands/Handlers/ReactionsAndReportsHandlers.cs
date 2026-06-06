using Core.Bases;
using Data.Enums;
using FluentValidation;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;

// ==================== REACTIONS ====================
namespace Core.Features.Community.Reactions.Commands.Models
{
    public class ToggleReactionCommand : IRequest<Response<string>>
    {
        public ReactionTargetType TargetType { get; set; }
        public int TargetId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}

namespace Core.Features.Community.Reactions.Commands.Handlers
{
    using Core.Features.Community.Reactions.Commands.Models;

    public class ReactionCommandHandler : IRequestHandler<ToggleReactionCommand, Response<string>>
    {
        private readonly ICommunityReactionService _reactionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ResponseHandler _responseHandler;

        public ReactionCommandHandler(
            ICommunityReactionService reactionService,
            ICurrentUserService currentUserService,
            ResponseHandler responseHandler)
        {
            _reactionService = reactionService;
            _currentUserService = currentUserService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(ToggleReactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            await _reactionService.ToggleReactionAsync(userId, request.TargetType, request.TargetId, request.ReactionType);
            return _responseHandler.Success<string>("تم تسجيل الإعجاب");
        }
    }
}

// ==================== REPORTS ====================
namespace Core.Features.Community.Reports.Commands.Models
{
    public class CreateReportCommand : IRequest<Response<string>>
    {
        public ReportTargetType TargetType { get; set; }
        public int TargetId { get; set; }
        public string Reason { get; set; }
    }
}

namespace Core.Features.Community.Reports.Commands.Validators
{
    using Core.Features.Community.Reports.Commands.Models;

    public class CreateReportValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.TargetId).GreaterThan(0).WithMessage("Id غير صحيح");
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("سبب البلاغ مطلوب")
                .MaximumLength(500).WithMessage("السبب يجب ألا يتجاوز 500 حرف");
        }
    }
}

namespace Core.Features.Community.Reports.Commands.Handlers
{
    using Core.Features.Community.Reports.Commands.Models;

    public class ReportCommandHandler : IRequestHandler<CreateReportCommand, Response<string>>
    {
        private readonly ICommunityReportService _reportService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ResponseHandler _responseHandler;

        public ReportCommandHandler(
            ICommunityReportService reportService,
            ICurrentUserService currentUserService,
            ResponseHandler responseHandler)
        {
            _reportService = reportService;
            _currentUserService = currentUserService;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            await _reportService.CreateAsync(userId, request.TargetType, request.TargetId, request.Reason);
            return _responseHandler.Created<string>("تم إرسال البلاغ بنجاح");
        }
    }
}
