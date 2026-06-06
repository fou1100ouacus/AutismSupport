using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Core.Behaviors;
using Core.Bases;
using System.Reflection;
using Service.Abstracts;
using Service.Implementations;

namespace Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // Configuration Of Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // Configuration Of Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Pipeline Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // ResponseHandler
            services.AddTransient<ResponseHandler>();

            return services;
        }
    }
}
