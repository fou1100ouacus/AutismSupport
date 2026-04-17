using Microsoft.Extensions.DependencyInjection;
// using Data.Entities.Views;
using Infrustructure.Abstracts;
// using Infrustructure.Abstracts.Procedures;
using Infrustructure.Abstracts.Views;
using Infrustructure.InfrastructureBases;
using Infrustructure.Repositories;


namespace Infrustructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();

            // services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            // //views

            // //Procedure

            //functions
          //  services.AddTransient<IInstructorFunctionsRepository, InstructorFunctionsRepository>();

            return services;
        }
    }
}