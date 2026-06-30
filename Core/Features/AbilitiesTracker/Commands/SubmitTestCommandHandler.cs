
using MediatR;
using Core.Bases;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;
using Service.Abstracts;
using Service.AuthServices.Interfaces;
using Data.Entities.Child;

namespace Core.Features.AbilitiesTracker.Commands
{
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, Response<TestResultResponseDto>>
    {
        private readonly IAbilityService _abilityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IChildService _childService;

        public SubmitTestCommandHandler(IAbilityService abilityService, ICurrentUserService currentUserService, IChildService childService)
        {
            _abilityService = abilityService;
            _currentUserService = currentUserService;
            _childService = childService;
        }

        public async Task<Response<TestResultResponseDto>> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            // 1. Get mother's ID from JWT token and retrieve her child profile
            var motherId = _currentUserService.GetUserId();
            var childProfile = await _childService.GetProfileByMotherIdAsync(motherId);
            
            if (childProfile == null)
            {
                return new Response<TestResultResponseDto>
                {
                    Succeeded = false,
                    Message = "Child profile not found for the authenticated mother"
                };
            }

            var childId = childProfile.Id;

            // 2. جلب قائمة الأسئلة بالكامل من الداتا بيز شاملة الـ Categories لعمل الـ Mapping الموثوق
            var dbQuestions = await _abilityService.GetAllQuestionsWithCategoriesAsync();

            // 2. تجهيز لستة لحساب نتائج الأقسام (Categories)
            var categoryGroups = dbQuestions.GroupBy(q => q.Category.NameEn);
            var observedBehaviors = new List<CategoryScoreDto>();
            int totalScore = 0;

            // 🛠️ تعديل: حساب أعلى درجة ممكنة بناءً على عدد الأسئلة المبعوثة فعلياً من الـ Frontend (مثلاً 15 سؤال)
            int maxPossibleScore = dto.Answers.Count * 2; 

            foreach (var group in categoryGroups)
            {
                string categoryName = group.Key;
                // جلب الـ IDs الخاصة بأسئلة هذا القسم المتاحة في الداتا بيز
                var questionIds = group.Select(q => q.Id).ToList();

                // جمع درجات المستخدم الخاصة بأسئلة هذا القسم فقط
                int categoryScore = dto.Answers
                    .Where(a => questionIds.Contains(a.QuestionId))
                    .Sum(a => a.AnswerValue);

                totalScore += categoryScore;

                // 🛠️ تعديل ديناميكي: احسب كم سؤال أرسله الـ Frontend يخص هذا القسم بالتحديد
                int answeredQuestionsInGroup = dto.Answers.Count(a => questionIds.Contains(a.QuestionId));
                
                // إذا كان القسم يحتوي على أسئلة مبعوثة، قم بحسابه، وإلا تخطى النسبة لتجنب القسمة على صفر
                if (answeredQuestionsInGroup > 0)
                {
                    int maxCategoryScore = answeredQuestionsInGroup * 2;
                    double categoryPercentage = ((double)categoryScore / maxCategoryScore) * 100;

                    // إذا حقق الطفل 60% أو أكثر من مهارات القسم يعتبر طبيعي، وإلا فهو يحتاج إلى دعم
                    string status = categoryPercentage >= 60.0 ? "Typical" : "Needs Support";

                    observedBehaviors.Add(new CategoryScoreDto
                    {
                        CategoryName = categoryName,
                        CategoryScore = categoryScore,
                        Status = status
                    });
                }
            }

            // 3. حساب النسبة المئوية الكلية والـ Risk Level
            // تجنب القسمة على صفر في حالة عدم إرسال أي إجابات
            double totalPercentage = maxPossibleScore > 0 ? ((double)totalScore / maxPossibleScore) * 100 : 0;
            totalPercentage = Math.Round(totalPercentage, 2); // تقريب لرقمان عشريان مثل 97.78

            // 🔥 تعديل منطقي طبي: كلما قلت نسبة امتلاك المهارات، كلما زاد خطر التأخر النموذجي (Risk Level)
            string riskLevel = "High";
            if (totalPercentage >= 75.0)
            {
                riskLevel = "Low"; // مهاراته ممتازة، الخطر منخفض جداً
            }
            else if (totalPercentage >= 50.0 && totalPercentage < 75.0)
            {
                riskLevel = "Medium"; // مهاراته متوسطة، الخطر متوسط
            }
            else
            {
                riskLevel = "High"; // مهاراته ضعيفة (أقل من 50%)، الخطر عالٍ
            }

            // 4. بناء الـ Entity الخاص بالداتا بيز وحفظه
            var testResultEntity = new Data.Entities.AbilitiesTracker.AbilityTestResult
            {
                ChildId = childId,
                TotalScore = totalScore,
                TotalPercentage = (float)totalPercentage, 
                Level = riskLevel,
                TestDate = DateTime.Now
            };

            // تمرير الأوبجكت الفردي الصحيح للميثود لتجنب خطأ الـ Arguments
            await _abilityService.SaveTestResultAsync(testResultEntity);

            // 5. بناء الـ Response النهائي ليرجع للـ Frontend
            var resultData = new TestResultResponseDto
            {
                RiskLevel = riskLevel,
                TotalPercentage = totalPercentage,
                ObservedBehaviors = observedBehaviors
            };

            return new Response<TestResultResponseDto>
            {
                Succeeded = true,
                Message = "Test submitted and analyzed successfully.",
                Data = resultData
            };
        }
    }
}