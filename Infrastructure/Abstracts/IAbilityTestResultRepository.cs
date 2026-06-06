using Data.Entities.AbilitiesTracker;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Abstracts
{
    public interface IAbilityTestResultRepository : IGenericRepositoryAsync<AbilityTestResult>
    {
        // جلب كافة نتائج اختبارات طفل معين لعرضها في التقارير
        Task<List<AbilityTestResult>> GetResultsByChildIdAsync(int childId);
        
        // جلب آخر نتيجة اختبار لقسم معين لمعرفة مدى التطور
        Task<AbilityTestResult?> GetLatestResultByChildIdAsync(int childId, int categoryId);
    }
}