using MediatR;
using System;
using System.Collections.Generic;
using Core.Bases;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class GetAbilityTestHistoryQuery : IRequest<Response<IEnumerable<AbilityTestHistoryDto>>>
    {
        public int ChildId { get; set; }
    }

    public class AbilityTestHistoryDto
    {
        public int TestResultId { get; set; }
        public DateTime TestDate { get; set; }
        public int TotalScore { get; set; }
        public double TotalPercentage { get; set; }
        public string Level { get; set; } = string.Empty; // Low, Medium, High
    }
}