using Data.Entities.AbilitiesTracker;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Abstracts
{
    public interface IAbilityRepository : IGenericRepositoryAsync<AbilityCategory>
    {
        // جلب الأقسام مع الأسئلة لعرضها في شاشة التقييم
        // Task<List<AbilityCategory>> GetCategoriesWithQuestionsAsync();
        // جلب الأقسام فقط بدون الأسئلة
        Task<List<AbilityCategory>> GetOnlyCategoriesAsync();
        // جلب قسم معين بأسئلته
        Task<AbilityCategory?> GetCategoryWithQuestionsByIdAsync(int categoryId);

        Task<List<AbilityQuestion>> GetQuestionsByCategoryIdAsync(int categoryId);
    }
}