using MediatR;
using Core.Bases;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class SubmitTestCommand : IRequest<Response<TestResultResponseDto>>
    {
        public SubmitTestRequestDto Dto { get; set; } = null!;
    }
}