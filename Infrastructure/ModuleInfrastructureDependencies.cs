using Microsoft.Extensions.DependencyInjection;
// using Data.Entities.Views;
using Infrastructure.Abstracts;
// using Infrastructure.Abstracts.Procedures;
using Infrastructure.Abstracts.Views;
using Infrastructure.InfrastructureBases;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();

            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IChildProfileRepository, ChildProfileRepository>();
         //   services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IAbilityRepository, AbilityRepository>();
            services.AddTransient<IAbilityTestResultRepository, AbilityTestResultRepository>();
            // //views

            // //Procedure

            //functions

            return services;
        }
    }
}