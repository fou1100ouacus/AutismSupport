using Data.Entities.AbilitiesTracker;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Abstracts
{
    public interface IAbilityQuestionRepository : IGenericRepositoryAsync<AbilityQuestion>
    {
        // يمكن إضافة دوال خاصة هنا إذا لزم الأمر
    }
}
