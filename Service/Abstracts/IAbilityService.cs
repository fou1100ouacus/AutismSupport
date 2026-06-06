using Data.Entities.AbilitiesTracker;

namespace Service.Abstracts
{
    public interface IAbilityService
    {  


        // جلب الأقسام والأسئلة لعرضها للأم
        Task<List<AbilityCategory>> GetAbilitiesCategoriesOnlyAsync();
        Task<List<AbilityTestResultDto>> GetHistoryByMotherAsync(int id);

        // معالجة وحفظ نتيجة التقييم
     //   Task<string> AddTestResultAsync(int childId, int categoryId, Dictionary<int, int> answers);
        Task<string> AddTestResultByMotherAsync(int motherId, int categoryId, Dictionary<int, int> answers);
   
        Task<List<AbilityQuestion>> GetQuestionsAsync(int categoryId);
    }
}