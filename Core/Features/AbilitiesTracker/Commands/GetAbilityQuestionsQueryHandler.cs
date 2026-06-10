using Core.Bases;
using Service.Abstracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class GetAbilityQuestionsQueryHandler : IRequestHandler<GetAbilityQuestionsQuery, Response<IEnumerable<AbilityQuestionDto>>>
    {
        private readonly IAbilityService _abilityService;

        public GetAbilityQuestionsQueryHandler(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }

        public async Task<Response<IEnumerable<AbilityQuestionDto>>> Handle(GetAbilityQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _abilityService.GetAllQuestionsWithCategoriesAsync();

            var questionDtos = questions.Select(q => new AbilityQuestionDto
            {
                Id = q.Id,
                QuestionTextEn = q.QuestionTextEn,
           //     CategoryId = q.CategoryId,
                CategoryNameEn = q.Category?.NameEn ?? string.Empty
            }).ToList();

            return new Response<IEnumerable<AbilityQuestionDto>>
            {
                Succeeded = true,
                Message = "Questions retrieved successfully.",
                Data = questionDtos
            };
        }
    }
}
