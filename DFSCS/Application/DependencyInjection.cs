using Application.Common.Behaviors;
using Application.Features.Answer.Command;
using Application.Features.Checklist.Queries;
using Application.Features.Option.Queries;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register MediatR (for CQRS)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // Register AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Register Validation Behavior globally for all MediatR requests
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped<Getchecklist>();
            services.AddScoped<Getoptionvalues>();
            services.AddScoped<InsertAnswer>();
         
            return services;
        }
    }
}
