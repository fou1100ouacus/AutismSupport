// using Core.Features.ChildProfile.Models;
// using Core.Bases;
// using Infrastructure.Abstracts;
// using Service.Abstracts;
// using MediatR;
// using System.Threading;
// using System.Threading.Tasks;
// using System;
// using Service.AuthServices.Interfaces;

// namespace Core.Features.ChildProfile.Queries
// {
//     public class GetChildProfileQueryHandler : IRequestHandler<GetChildProfileQuery, Response<CreateChildProfileDto>>
//     {
//         private readonly ICurrentUserService _currentUserService;
//         private readonly IChildService _childService;

//         public GetChildProfileQueryHandler(ICurrentUserService currentUserService, IChildService childService)
//         {
//             _currentUserService = currentUserService;
//             _childService = childService;
//         }

//         public async Task<Response<CreateChildProfileDto>> Handle(GetChildProfileQuery request, CancellationToken cancellationToken)
//         {
//             var userId = _currentUserService.GetUserId();
            
//             // Debug: Log the userId being used
//             Console.WriteLine($"DEBUG: Retrieved userId from token: {userId}");
            
//             var childProfile = await _childService.GetProfileByMotherIdAsync(userId);
            
//             // Debug: Log if profile was found
//             Console.WriteLine($"DEBUG: ChildProfile found for userId {userId}: {childProfile != null}");
            
//             if (childProfile == null)
//             {
//                 return new Response<CreateChildProfileDto>
//                 {
//                     Succeeded = false,
//                     Message = $"Child profile not found for mother ID: {userId}"
//                 };
//             }

//             var dto = new CreateChildProfileDto
//             {
//                 Nickname = childProfile.Nickname,
//                 AgeInYears = childProfile.AgeInYears,
//                 AgeInMonths = childProfile.AgeInMonths,
//                 Gender = childProfile.Gender,
//                 SupportNeedsLevel = childProfile.SupportNeedsLevel,
//                 MainDailyChallengesJson = childProfile.MainDailyChallengesJson,
//                 StrengthsAndInterests = childProfile.StrengthsAndInterests,
//                 PrefersVisualSchedules = childProfile.PrefersVisualSchedules,
//                 CommunicationMethodsJson = childProfile.CommunicationMethodsJson
//             };

//             return new Response<CreateChildProfileDto>
//             {
//                 Succeeded = true,
//                 Data = dto,
//                 Message = "Child profile retrieved successfully"
//             };
//         }
//     }
// }



using Core.Features.ChildProfile.Models;
using Core.Bases;
using Infrastructure.Abstracts;
using Service.Abstracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic; // تم إضافتها لاستخدام الـ List
using System.Text.Json;        // تم إضافتها لتحويل وقراءة الـ JSON
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
            
            // Debug: Log the userId being used
            Console.WriteLine($"DEBUG: Retrieved userId from token: {userId}");
            
            var childProfile = await _childService.GetProfileByMotherIdAsync(userId);
            
            // Debug: Log if profile was found
            Console.WriteLine($"DEBUG: ChildProfile found for userId {userId}: {childProfile != null}");
            
            if (childProfile == null)
            {
                return new Response<CreateChildProfileDto>
                {
                    Succeeded = false,
                    Message = $"Child profile not found for mother ID: {userId}"
                };
            }

            // فك ضغط النصوص وتحويلها إلى مصفوفات C# حقيقية للـ DTO الجديد
            var dailyChallenges = string.IsNullOrEmpty(childProfile.MainDailyChallengesJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(childProfile.MainDailyChallengesJson);

            var commMethods = string.IsNullOrEmpty(childProfile.CommunicationMethodsJson)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(childProfile.CommunicationMethodsJson);

            var strengthsList = string.IsNullOrEmpty(childProfile.StrengthsAndInterests)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(childProfile.StrengthsAndInterests);

            var dto = new CreateChildProfileDto
            {
                Nickname = childProfile.Nickname,
                AgeInYears = childProfile.AgeInYears,
                AgeInMonths = childProfile.AgeInMonths,
                Gender = childProfile.Gender,
                SupportNeedsLevel = childProfile.SupportNeedsLevel,
                PrefersVisualSchedules = childProfile.PrefersVisualSchedules,
                
                // ربط القوائم الجديدة بعد فكها بنجاح
                MainDailyChallenges = dailyChallenges!,
                CommunicationMethods = commMethods!,
                StrengthsAndInterests = strengthsList!
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