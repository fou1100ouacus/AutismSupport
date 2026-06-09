using MediatR;
using System.Collections.Generic;
using Core.Bases;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class GetAbilityQuestionsQuery : IRequest<Response<IEnumerable<AbilityQuestionDto>>>
    {
    }

    public class AbilityQuestionDto
    {
        public int Id { get; set; }
        public string QuestionTextEn { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryNameEn { get; set; } = string.Empty;
    }
}