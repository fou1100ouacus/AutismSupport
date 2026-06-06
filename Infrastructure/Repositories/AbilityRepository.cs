// using Infrastructure.Abstracts;
// using Data.Entities.AbilitiesTracker;
// using Infrastructure.Context;
// using Infrastructure.InfrastructureBases;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Repositories
// {
//     public class AbilityRepository : GenericRepositoryAsync<AbilityCategory>, IAbilityRepository
//     {
//         private readonly DbSet<AbilityCategory> _categories;
//         private readonly ApplicationDBContext _dbContext; 
//         public AbilityRepository(ApplicationDBContext dbContext) : base(dbContext)
//         {
//             _dbContext = dbContext;
//             _categories = dbContext.Set<AbilityCategory>();
//         }
//         public async Task<List<AbilityCategory>> GetOnlyCategoriesAsync()
//         {
//             return await _categories.ToListAsync();
//         }
//         // public async Task<List<AbilityCategory>> GetCategoriesWithQuestionsAsync()
//         // {
//         //     return await _categories
//         //         .Include(x => x.Questions)
//         //         .ToListAsync();
//         // }
//             public async Task<List<AbilityQuestion>> GetQuestionsByCategoryIdAsync(int categoryId)
//             {
//                 return await _dbContext.AbilityQuestions
//                 .Where(q => q.CategoryId == categoryId)
//                 .ToListAsync();
//             }
//         public async Task<AbilityCategory?> GetCategoryWithQuestionsByIdAsync(int categoryId)
//         {
//             return await _categories
//                 .Include(x => x.Questions)
//                 .FirstOrDefaultAsync(x => x.Id == categoryId);
//         }
//     }
// }

using Infrastructure.Abstracts;
using Data.Entities.AbilitiesTracker;
using Infrastructure.Context;
using Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AbilityRepository : GenericRepositoryAsync<AbilityCategory>, IAbilityRepository
    {
        private readonly DbSet<AbilityCategory> _categories;

        public AbilityRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            // نستخدم _categories فقط لأن _dbContext موروث وجاهز للاستخدام
            _categories = dbContext.Set<AbilityCategory>();
        }

        public async Task<List<AbilityCategory>> GetOnlyCategoriesAsync()
        {
            return await _categories.ToListAsync();
        }

        public async Task<List<AbilityQuestion>> GetQuestionsByCategoryIdAsync(int categoryId)
        {
            // نستخدم _dbContext الموروث من GenericRepositoryAsync مباشرة
            return await _dbContext.AbilityQuestions
                .Where(q => q.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<AbilityCategory?> GetCategoryWithQuestionsByIdAsync(int categoryId)
        {
            return await _categories
                .Include(x => x.Questions)
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}