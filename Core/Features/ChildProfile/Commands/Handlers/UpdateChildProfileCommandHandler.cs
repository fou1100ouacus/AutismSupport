// using Core.Features.ChildProfile.Models;
// using Core.Bases;
// using MediatR;
// using Service.Abstracts;
// using Service.AuthServices.Interfaces;
// using Data.Entities.Child;

// namespace Core.Features.ChildProfile.Commands.Handlers
// {
//     public class UpdateChildProfileCommandHandler : IRequestHandler<UpdateChildProfileCommand, Response<string>>
//     {
//         private readonly ICurrentUserService _currentUserService;
//         private readonly IChildService _childService;

//         public UpdateChildProfileCommandHandler(ICurrentUserService currentUserService, IChildService childService)
//         {
//             _currentUserService = currentUserService;
//             _childService = childService;
//         }

//         public async Task<Response<string>> Handle(UpdateChildProfileCommand request, CancellationToken cancellationToken)
//         {
//             var userId = _currentUserService.GetUserId();
            
//             // Check if user has an existing profile
//             var existingProfile = await _childService.GetProfileByMotherIdAsync(userId);
//             if (existingProfile == null)
//             {
//                 return new Response<string>
//                 {
//                     Succeeded = false,
//                     Message = "No child profile found to update. Please create a profile first."
//                 };
//             }

//             // Update the existing profile
//             existingProfile.Nickname = request.Dto.Nickname;
//             existingProfile.AgeInYears = request.Dto.AgeInYears;
//             existingProfile.AgeInMonths = request.Dto.AgeInMonths;
//             existingProfile.Gender = request.Dto.Gender;
//             existingProfile.SupportNeedsLevel = request.Dto.SupportNeedsLevel;
//             existingProfile.MainDailyChallengesJson = request.Dto.MainDailyChallengesJson;
//             existingProfile.StrengthsAndInterests = request.Dto.StrengthsAndInterests;
//             existingProfile.PrefersVisualSchedules = request.Dto.PrefersVisualSchedules;
//             existingProfile.CommunicationMethodsJson = request.Dto.CommunicationMethodsJson;
//             existingProfile.LastUpdatedAt = DateTime.UtcNow;

//             // Note: You'll need to add an UpdateChildProfileAsync method to IChildService
//             // For now, let's assume it exists or create a basic implementation
//             try
//             {
//                 // This method needs to be implemented in your IChildService
//                 await _childService.UpdateChildProfileAsync(existingProfile);
                
//                 return new Response<string>
//                 {
//                     Succeeded = true,
//                     Message = "Child profile updated successfully"
//                 };
//             }
//             catch (Exception ex)
//             {
//                 return new Response<string>
//                 {
//                     Succeeded = false,
//                     Message = $"Failed to update child profile: {ex.Message}"
//                 };
//             }
//         }
//     }
// }



using Core.Features.ChildProfile.Models;
using Core.Bases;
using MediatR;
using Service.Abstracts;
using Service.AuthServices.Interfaces;
using Data.Entities.Child;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text.Json; // تم إضافتها هنا لاستخدام سحر الـ Serialization والضغط النصي

namespace Core.Features.ChildProfile.Commands.Handlers
{
    public class UpdateChildProfileCommandHandler : IRequestHandler<UpdateChildProfileCommand, Response<string>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IChildService _childService;

        public UpdateChildProfileCommandHandler(ICurrentUserService currentUserService, IChildService childService)
        {
            _currentUserService = currentUserService;
            _childService = childService;
        }

        public async Task<Response<string>> Handle(UpdateChildProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            
            // Check if user has an existing profile
            var existingProfile = await _childService.GetProfileByMotherIdAsync(userId);
            if (existingProfile == null)
            {
                return new Response<string>
                {
                    Succeeded = false,
                    Message = "No child profile found to update. Please create a profile first."
                };
            }

            // Update the existing profile
            existingProfile.Nickname = request.Dto.Nickname;
            existingProfile.AgeInYears = request.Dto.AgeInYears;
            existingProfile.AgeInMonths = request.Dto.AgeInMonths;
            existingProfile.Gender = request.Dto.Gender;
            existingProfile.SupportNeedsLevel = request.Dto.SupportNeedsLevel;
            existingProfile.PrefersVisualSchedules = request.Dto.PrefersVisualSchedules;
            existingProfile.LastUpdatedAt = DateTime.UtcNow;

            // ⚡ التعديل الاحترافي الموحد: ضغط المصفوفات الثلاث القادمة من الـ Chips لنصوص JSON متوافقة مع قاعدة البيانات
            existingProfile.MainDailyChallengesJson = JsonSerializer.Serialize(request.Dto.MainDailyChallenges);
            existingProfile.CommunicationMethodsJson = JsonSerializer.Serialize(request.Dto.CommunicationMethods);
            existingProfile.StrengthsAndInterests = JsonSerializer.Serialize(request.Dto.StrengthsAndInterests);

            try
            {
                // This method needs to be implemented in your IChildService
                await _childService.UpdateChildProfileAsync(existingProfile);
                
                return new Response<string>
                {
                    Succeeded = true,
                    Message = "Child profile updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    Succeeded = false,
                    Message = $"Failed to update child profile: {ex.Message}"
                };
            }
        }
    }
} 