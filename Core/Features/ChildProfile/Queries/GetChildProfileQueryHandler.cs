using Core.Features.ChildProfile.Models;
using Core.Bases;
using Infrastructure.Abstracts;
using Service.Abstracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using Service.AuthServices.Interfaces;

namespace Core.Features.ChildProfile.Queries
{
    public class GetChildProfileQueryHandler : IRequestHandler<GetChildProfileQuery, Response<CreateChildProfileDto>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IChildService _childService;

        public GetChildProfileQueryHandler(ICurrentUserService currentUserService, IChildService childService)
        {
            _currentUserService = currentUserService;
            _childService = childService;
        }

        public async Task<Response<CreateChildProfileDto>> Handle(GetChildProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            
            var childProfile = await _childService.GetProfileByMotherIdAsync(userId);
            
            if (childProfile == null)
            {
                return new Response<CreateChildProfileDto>
                {
                    Succeeded = false,
                    Message = "Child profile not found"
                };
            }

            var dto = new CreateChildProfileDto
            {
                Nickname = childProfile.Nickname,
                AgeInYears = childProfile.AgeInYears,
                AgeInMonths = childProfile.AgeInMonths,
                Gender = childProfile.Gender,
                SupportNeedsLevel = childProfile.SupportNeedsLevel,
                MainDailyChallengesJson = childProfile.MainDailyChallengesJson,
                StrengthsAndInterests = childProfile.StrengthsAndInterests,
                PrefersVisualSchedules = childProfile.PrefersVisualSchedules,
                CommunicationMethodsJson = childProfile.CommunicationMethodsJson
            };

            return new Response<CreateChildProfileDto>
            {
                Succeeded = true,
                Data = dto,
                Message = "Child profile retrieved successfully"
            };
        }
    }
}
