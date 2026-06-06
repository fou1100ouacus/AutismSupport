using Data.Entities.Child;
using Infrastructure.Abstracts;
using Service.Abstracts;

namespace Service.Implementations
{
    public class ChildService : IChildService
    {
        private readonly IChildProfileRepository _childProfileRepository;

        public ChildService(IChildProfileRepository childProfileRepository)
        {
            _childProfileRepository = childProfileRepository;
        }

        public async Task<string> AddChildProfileAsync(ChildProfile childProfile)
        {
            // 1. التحقق من وجود ملف مسبق (Business Rule)
            var exists = await _childProfileRepository.MotherHasProfileAsync(childProfile.MotherId);
            if (exists) return "Exists";

            // 2. إضافة الملف
            try 
            {
                await _childProfileRepository.AddAsync(childProfile);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public async Task<bool> IsMotherHasProfileAsync(int motherId)
        {
            return await _childProfileRepository.MotherHasProfileAsync(motherId);
        }

        public async Task<ChildProfile?> GetProfileByMotherIdAsync(int motherId)
        {
            return await _childProfileRepository.GetByMotherIdAsync(motherId);
        }

        public async Task<string> UpdateChildProfileAsync(ChildProfile childProfile)
        {
            try
            {
                await _childProfileRepository.UpdateAsync(childProfile);
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        
    }
}