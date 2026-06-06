using Infrastructure.Abstracts;
using Service.Abstracts;
using Data.Entities.AbilitiesTracker;
using System.Linq;
namespace Service.Implementations
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityRepository _abilityRepository;
        private readonly IAbilityTestResultRepository _resultRepository;
        private readonly IChildProfileRepository _childRepository;

        public AbilityService(IAbilityRepository abilityRepository, 
                              IAbilityTestResultRepository resultRepository,
                              IChildProfileRepository childRepository)
        {
            _abilityRepository = abilityRepository;
            _resultRepository = resultRepository;
            _childRepository = childRepository;
        }
   
        public async Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync()
        {
            return await _abilityRepository.GetOnlyCategoriesAsync();
        }

            public async Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId)
            {
                return await _abilityRepository.GetQuestionsByCategoryIdAsync(categoryId);
            }
        public async Task<string> AddTestResultAsync(int childId, int categoryId, Dictionary<int, int> answers)
        {
            // 1. حساب المجموع
            int totalScore = answers.Values.Sum();

            // 2. تحديد المستوى (منطق تجريبي بناءً على مجموع النقاط)
            string level = totalScore switch
            {
                >= 25 => "متقدم (High)",
                >= 15 => "متوسط (Medium)",
                _ => "يحتاج تطوير (Low)"
            };

            // 3. بناء الكيان
            var testResult = new AbilityTestResult
            {
                ChildId = childId,
                CategoryId = categoryId,
                TotalScore = totalScore,
                Level = level,
                TestDate = DateTime.UtcNow,
                DetailedAnswersJson = System.Text.Json.JsonSerializer.Serialize(answers)
            };

            // 4. الحفظ
            await _resultRepository.AddAsync(testResult);
            
            return "Success";
        }

      
        public async Task<List<AbilityTestResultDto>> GetHistoryByMotherAsync(int motherId)
        {
            // 1. البحث عن بروفايل الطفل باستخدام معرف الأم القادم من الـ Token
            var child = await _childRepository.GetByMotherIdAsync(motherId);
            
            if (child == null) return new List<AbilityTestResultDto>();

            // 2. جلب النتائج من الـ Repository
            var results = await _resultRepository.GetResultsByChildIdAsync(child.Id);

            // 3. التحويل لـ DTO لمنع الـ Cycle Error ولتنسيق البيانات للموبايل
            return results.Select(r => new AbilityTestResultDto
            {
                Id = r.Id,
                TotalScore = r.TotalScore,
                Level = r.Level,
                TestDate = r.TestDate,
    //            CategoryNameAr = r.Category?.NameAr ?? "غير معروف",
                CategoryNameEn = r.Category?.NameEn ?? "Unknown"
            }).ToList();
        }

         public async Task<string> AddTestResultByMotherAsync(int motherId, int categoryId, Dictionary<int, int> answers)
        {
            // 1. البحث عن الطفل المرتبط بالأم آلياً
            var child = await _childRepository.GetByMotherIdAsync(motherId);
            if (child == null) return "ChildNotFound";

            // 2. حساب المجموع الكلي
            int totalScore = answers.Values.Sum();

            // 3. تحديد المستوى (منطق حسابي)
            string level = totalScore switch
            {
                >= 20 => "متقدم (High)",
                >= 12 => "متوسط (Medium)",
                _ => "يحتاج اهتمام (Low)"
            };

            // 4. بناء كائن النتيجة
            var testResult = new AbilityTestResult
            {
                ChildId = child.Id,
                CategoryId = categoryId,
                TotalScore = totalScore,
                Level = level,
                TestDate = DateTime.UtcNow,
                // تخزين الإجابات كتفاصيل JSON إذا أردت الرجوع لها لاحقاً
                DetailedAnswersJson = System.Text.Json.JsonSerializer.Serialize(answers)
            };

            await _resultRepository.AddAsync(testResult);
            return "Success";
        }








    }
}