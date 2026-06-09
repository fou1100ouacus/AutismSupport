
using Data.Entities.AbilitiesTracker;
using Infrastructure.Abstracts; 
using Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class AbilityService : IAbilityService
    {
        private readonly IAbilityQuestionRepository _questionRepository;
        private readonly IAbilityTestResultRepository _testResultRepository;

        public AbilityService(
            IAbilityQuestionRepository questionRepository,
            IAbilityTestResultRepository testResultRepository)
        {
            _questionRepository = questionRepository;
            _testResultRepository = testResultRepository;
        }

        // 🔥 التعديل الاحترافي باستخدام ميزات الـ Generic Repository
        public async Task<IEnumerable<AbilityQuestion>> GetAllQuestionsWithCategoriesAsync()
        {
            // بنجيب جدول الأسئلة ونعمل Include للـ Category عشان الحسبة جوه الـ Handler تشتغل صح
            return await _questionRepository.GetTableNoTracking() // أو اسم الميثود اللي بترجع IQueryable عندك
                                            .Include(q => q.Category)
                                            .ToListAsync();
        }

        public async Task<bool> SaveTestResultAsync(AbilityTestResult testResult)
        {
            // الـ AddAsync موروثة وجاهزة من الـ Generic Repo
            await _testResultRepository.AddAsync(testResult);
            return true;
        }

        public async Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync() => throw new System.NotImplementedException();
        public async Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId) => throw new System.NotImplementedException();
public async Task<List<AbilityTestResult>> GetHistoryByMotherAsync(int childId)
{
    // بنجيب جدول النتائج وبنفلتر بالـ ChildId وبنرتبهم من الأحدث للأقدم
    return await _testResultRepository.GetTableNoTracking()
                                      .Where(r => r.ChildId == childId)
                                      .OrderByDescending(r => r.TestDate)
                                      .ToListAsync();
}    }
}