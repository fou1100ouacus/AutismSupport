// using Infrastructure.Abstracts;
// using Data.Entities.AbilitiesTracker;
// using Infrastructure.Context;
// using Infrastructure.InfrastructureBases;
// using Microsoft.EntityFrameworkCore;

// namespace Infrastructure.Repositories
// {
//     public class AbilityTestResultRepository : GenericRepositoryAsync<AbilityTestResult>, IAbilityTestResultRepository
//     {
//         private readonly DbSet<AbilityTestResult> _results;

//         public AbilityTestResultRepository(ApplicationDBContext dbContext) : base(dbContext)
//         {
//             _results = dbContext.Set<AbilityTestResult>();
//         }

//         public async Task<List<AbilityTestResult>> GetResultsByChildIdAsync(int childId)
//         {
//             return await _results
//                 .Where(x => x.ChildId == childId)
//                 .Include(x => x.Category)
//                 .OrderByDescending(x => x.TestDate)
//                 .ToListAsync();
//         }

//         public async Task<AbilityTestResult?> GetLatestResultByChildIdAsync(int childId, int categoryId)
//         {
//             return await _results
//                 .Where(x => x.ChildId == childId && x.CategoryId == categoryId)
//                 .OrderByDescending(x => x.TestDate)
//                 .FirstOrDefaultAsync();
//         }
//     }
// }


// using Data.Entities.AbilitiesTracker;
// using Infrastructure.Abstracts;
// using System.Threading.Tasks;

// namespace Infrastructure.Repositories
// {
//     public class AbilityTestResultRepository : IAbilityTestResultRepository
//     {
//         private readonly ApplicationDbContext _context; // 💡 نفس الـ DbContext بتاع مشروعك

//         public AbilityTestResultRepository(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task AddAsync(AbilityTestResult testResult)
//         {
//             await _context.AbilityTestResults.AddAsync(testResult);
//             await _context.SaveChangesAsync(); // حفظ السطر فوراً في الـ Database
//         }
//     }
// }

using Data.Entities.AbilitiesTracker;
using Infrastructure.Abstracts;
using Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class AbilityTestResultRepository : GenericRepositoryAsync<AbilityTestResult>, IAbilityTestResultRepository
    {
        private readonly DbSet<AbilityTestResult> _abilityTestResults;

        public AbilityTestResultRepository(ApplicationDBContext context) : base(context)
        {
            _abilityTestResults = context.Set<AbilityTestResult>();
        }
    }
}