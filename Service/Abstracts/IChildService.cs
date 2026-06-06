using Data.Entities.Child;
namespace Service.Abstracts
{
    public interface IChildService
    {
        Task<string> AddChildProfileAsync(ChildProfile childProfile);
        Task<bool> IsMotherHasProfileAsync(int motherId);
        Task<ChildProfile?> GetProfileByMotherIdAsync(int motherId);
        Task<string> UpdateChildProfileAsync(ChildProfile childProfile);
    }
}