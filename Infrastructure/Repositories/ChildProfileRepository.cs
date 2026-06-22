using Data.Entities.Child;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Infrastructure.Abstracts;
using Infrastructure.Context;
using Infrastructure.InfrastructureBases;

namespace Infrastructure.Repositories
{
    public class ChildProfileRepository : GenericRepositoryAsync<ChildProfile>, IChildProfileRepository
    { 

        private readonly DbSet<ChildProfile> _childProfiles;
        public ChildProfileRepository(ApplicationDBContext  dbContext) 
            : base(dbContext)
        {        
          //    _childProfiles=dbContext.Set<ChildProfile>();

        }

        public async Task<ChildProfile?> GetByMotherIdAsync(int motherId)
        {
            Console.WriteLine($"DEBUG: Repository querying ChildProfile for MotherId: {motherId}");
            var result = await _dbContext.ChildProfile
                .FirstOrDefaultAsync(c => c.MotherId == motherId);
            Console.WriteLine($"DEBUG: Repository query result for MotherId {motherId}: {result != null}");
            return result;
        }

        public async Task<bool> MotherHasProfileAsync(int motherId)
        {
            return await _dbContext.ChildProfile
                .AnyAsync(c => c.MotherId == motherId);
        }
    }
}