// using Data.Entities.AbilitiesTracker;
// using Infrastructure.InfrastructureBases;

// namespace Infrastructure.Abstracts
// {
//     public interface IAbilityTestResultRepository : IGenericRepositoryAsync<AbilityTestResult>
//     {
//         // جلب كافة نتائج اختبارات طفل معين لعرضها في التقارير
//         Task<List<AbilityTestResult>> GetResultsByChildIdAsync(int childId);
        
//         // جلب آخر نتيجة اختبار لقسم معين لمعرفة مدى التطور
//         Task<AbilityTestResult?> GetLatestResultByChildIdAsync(int childId, int categoryId);
//     }
// }

// using Data.Entities.AbilitiesTracker;
// using System.Threading.Tasks;

// namespace Infrastructure.Abstracts
// {
//     public interface IAbilityTestResultRepository
//     {
//         // إضافة نتيجة الفحص المجمعة في الداتا بيز
//         Task AddAsync(AbilityTestResult testResult);
//     }
// }

using Data.Entities.AbilitiesTracker;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Abstracts
{
    public interface IAbilityTestResultRepository : IGenericRepositoryAsync<AbilityTestResult>
    {
        // الـ Generic Repository مغطي الـ AddAsync والـ SaveChanges تلقائياً
    }
}