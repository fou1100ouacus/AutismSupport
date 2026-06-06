using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Abstracts;
using Infrastructure.Abstracts.Views;
using Infrastructure.InfrastructureBases;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            // Existing
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IChildProfileRepository, ChildProfileRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            // Community Repositories
            services.AddTransient<ICommunityPostRepository, CommunityPostRepository>();
            services.AddTransient<ICommunityCommentRepository, CommunityCommentRepository>();
            services.AddTransient<ICommunityReactionRepository, CommunityReactionRepository>();
            services.AddTransient<ICommunityReportRepository, CommunityReportRepository>();

            return services;
        }
    }
}
