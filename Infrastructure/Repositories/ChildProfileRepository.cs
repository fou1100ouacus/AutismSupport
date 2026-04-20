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
            return await _dbContext.ChildProfiles
                .FirstOrDefaultAsync(c => c.MotherId == motherId);
        }

        public async Task<bool> MotherHasProfileAsync(int motherId)
        {
            return await _dbContext.ChildProfiles
                .AnyAsync(c => c.MotherId == motherId);
        }
    }
}