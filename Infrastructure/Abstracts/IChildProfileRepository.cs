using Data.Entities.Child;
using Infrastructure.InfrastructureBases;
namespace Infrastructure.Abstracts
{
public interface IChildProfileRepository : IGenericRepositoryAsync<ChildProfile>
{
    // طريقة خاصة بـ ChildProfile
    Task<ChildProfile?> GetByMotherIdAsync(int motherId);
    
    Task<bool> MotherHasProfileAsync(int motherId);   // هل الأم لها بروفايل بالفعل؟
}
}