using Microsoft.Extensions.DependencyInjection;
using Service.Abstracts;
using Service.AuthServices.Implementations;
using Service.AuthServices.Interfaces;
using Service.Implementations;

namespace Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailsService, EmailsService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IChildService, ChildService>();
            return services;
        }
    }
}