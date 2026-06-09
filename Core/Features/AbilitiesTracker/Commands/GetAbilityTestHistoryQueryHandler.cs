using Core.Bases;
using Service.Abstracts;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class GetAbilityTestHistoryQueryHandler : IRequestHandler<GetAbilityTestHistoryQuery, Response<IEnumerable<AbilityTestHistoryDto>>>
    {
        private readonly IAbilityService _abilityService;

        public GetAbilityTestHistoryQueryHandler(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }

        public async Task<Response<IEnumerable<AbilityTestHistoryDto>>> Handle(GetAbilityTestHistoryQuery request, CancellationToken cancellationToken)
        {
            // جلب التاريخ من الـ Service Layer
            var history = await _abilityService.GetHistoryByMotherAsync(request.ChildId);

            var historyDtos = history.Select(h => new AbilityTestHistoryDto
            {
                TestResultId = h.Id,
                TestDate = h.TestDate,
                TotalScore = h.TotalScore,
                TotalPercentage = (double)h.TotalPercentage,
                Level = h.Level
            }).ToList();

            return new Response<IEnumerable<AbilityTestHistoryDto>>
            {
                Succeeded = true,
          //      StatusCode = 200,
                Message = "Test history retrieved successfully.",
                Data = historyDtos
            };
        }
    }
}