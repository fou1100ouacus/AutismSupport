// using Core.Bases;
// using Core.Features.ChildProfile.Commands;
// using Core.Features.ChildProfile.Models;
// using Data.Entities.Child;
// using Infrastructure.Abstracts;
// using Service.Abstracts;
// using MediatR;
// using System.Threading;
// using System.Threading.Tasks;
// using System;
// using Service.AuthServices.Interfaces;
// namespace Core.Features.ChildProfile.Handlers
// {
//     public class AddChildProfileCommandHandler : IRequestHandler<AddChildProfileCommand, Response<int>>
//     {
//         private readonly IChildService _childProfileService; // استخدام الخدمة بدلاً من الـ Repository
//         private readonly ICurrentUserService _currentUserService;

//         public AddChildProfileCommandHandler(
//             IChildService childProfileService, 
//             ICurrentUserService currentUserService)
//         {
//             _childProfileService = childProfileService;
//             _currentUserService = currentUserService;
//         }

//         public async Task<Response<int>> Handle(AddChildProfileCommand request, CancellationToken cancellationToken)
//         {
//             // 1. جلب معرف المستخدم الحالي (الأم)
//             var currentMotherId = _currentUserService.GetUserId();

//             // 2. تحويل الـ DTO إلى Entity
//             var childProfile = new Data.Entities.Child.ChildProfile
//             {
//                 MotherId = currentMotherId,
//                 Nickname = request.Dto.Nickname,
//                 AgeInYears = request.Dto.AgeInYears,
//                 AgeInMonths = request.Dto.AgeInMonths,
//                 Gender = request.Dto.Gender,
//                 SupportNeedsLevel = request.Dto.SupportNeedsLevel,
//                 MainDailyChallengesJson = request.Dto.MainDailyChallengesJson,
//                 StrengthsAndInterests = request.Dto.StrengthsAndInterests,
//                 PrefersVisualSchedules = request.Dto.PrefersVisualSchedules,
//                 CommunicationMethodsJson = request.Dto.CommunicationMethodsJson,
//                 CreatedAt = DateTime.UtcNow,
//                 LastUpdatedAt = DateTime.UtcNow
//             };

//             // 3. استدعاء الخدمة لتنفيذ منطق العمل والحفظ
//             var result = await _childProfileService.AddChildProfileAsync(childProfile);

//             // 4. معالجة الرد بناءً على نتيجة الخدمة
//             return result switch
//             {
//                 "Success" => new Response<int>(childProfile.Id, "Successful created ."),
//                 "Exists" => new Response<int>("Child profile with the same nickname already exists."),
//                 _ => new Response<int>("Error occurred while creating child profile")
//             };
//         }
//     }
// }



using Core.Bases;
using Core.Features.ChildProfile.Commands;
using Core.Features.ChildProfile.Models;
using Data.Entities.Child;
using Infrastructure.Abstracts;
using Service.Abstracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text.Json; // تم إضافتها هنا لتنفيذ سحر الـ Serialization (ضغط المصفوفات)
using Service.AuthServices.Interfaces;

namespace Core.Features.ChildProfile.Handlers
{
    public class AddChildProfileCommandHandler : IRequestHandler<AddChildProfileCommand, Response<int>>
    {
        private readonly IChildService _childProfileService; // استخدام الخدمة بدلاً من الـ Repository
        private readonly ICurrentUserService _currentUserService;

        public AddChildProfileCommandHandler(
            IChildService childProfileService, 
            ICurrentUserService currentUserService)
        {
            _childProfileService = childProfileService;
            _currentUserService = currentUserService;
        }

        public async Task<Response<int>> Handle(AddChildProfileCommand request, CancellationToken cancellationToken)
        {
            // 1. جلب معرف المستخدم الحالي (الأم)
            var currentMotherId = _currentUserService.GetUserId();

            // 2. تحويل الـ DTO إلى Entity مع تطبيق ضغط المصفوفات لنصوص JSON
            var childProfile = new Data.Entities.Child.ChildProfile
            {
                MotherId = currentMotherId,
                Nickname = request.Dto.Nickname,
                AgeInYears = request.Dto.AgeInYears,
                AgeInMonths = request.Dto.AgeInMonths,
                Gender = request.Dto.Gender,
                SupportNeedsLevel = request.Dto.SupportNeedsLevel,
                PrefersVisualSchedules = request.Dto.PrefersVisualSchedules,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow,

                // ⚡ التعديل الاحترافي هنا: تحويل الـ List<string> إلى نصوص قبل الحفظ بـ Database
                MainDailyChallengesJson = JsonSerializer.Serialize(request.Dto.MainDailyChallenges),
                CommunicationMethodsJson = JsonSerializer.Serialize(request.Dto.CommunicationMethods),
                StrengthsAndInterests = JsonSerializer.Serialize(request.Dto.StrengthsAndInterests)
            };

            // 3. استدعاء الخدمة لتنفيذ منطق العمل والحفظ
            var result = await _childProfileService.AddChildProfileAsync(childProfile);

            // 4. معالجة الرد بناءً على نتيجة الخدمة
            return result switch
            {
                "Success" => new Response<int>(childProfile.Id, "Successful created ."),
                "Exists" => new Response<int>("Child profile with the same nickname already exists."),
                _ => new Response<int>("Error occurred while creating child profile")
            };
        }
    }
}