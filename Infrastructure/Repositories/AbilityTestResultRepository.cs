using Infrastructure.Abstracts;
using Data.Entities.AbilitiesTracker;
using Infrastructure.Context;
using Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AbilityTestResultRepository : GenericRepositoryAsync<AbilityTestResult>, IAbilityTestResultRepository
    {
        private readonly DbSet<AbilityTestResult> _results;

        public AbilityTestResultRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _results = dbContext.Set<AbilityTestResult>();
        }

        public async Task<List<AbilityTestResult>> GetResultsByChildIdAsync(int childId)
        {
            return await _results
                .Where(x => x.ChildId == childId)
                .Include(x => x.Category)
                .OrderByDescending(x => x.TestDate)
                .ToListAsync();
        }

        public async Task<AbilityTestResult?> GetLatestResultByChildIdAsync(int childId, int categoryId)
        {
            return await _results
                .Where(x => x.ChildId == childId && x.CategoryId == categoryId)
                .OrderByDescending(x => x.TestDate)
                .FirstOrDefaultAsync();
        }
    }
}