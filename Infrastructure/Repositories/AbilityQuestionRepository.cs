using Data.Entities.AbilitiesTracker;
using Infrastructure.Abstracts;
using Infrastructure.InfrastructureBases;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class AbilityQuestionRepository : GenericRepositoryAsync<AbilityQuestion>, IAbilityQuestionRepository
    {
        private readonly DbSet<AbilityQuestion> _abilityQuestions;

        public AbilityQuestionRepository(ApplicationDBContext context) : base(context)
        {
            _abilityQuestions = context.Set<AbilityQuestion>();
        }
    }
}