using System.Reflection;
using Application.Common.Behaviors;
using Application.Common.Mappings;
using Application.TestUsers.Queries.CheckUsernameAvailable;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        AddCustomBehavior(services);

        return services;
    }

    private static void AddCustomBehavior(IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<CheckUsernameAvailableQuery, ErrorOr<bool>>), typeof(CheckUsernameAvailableQueryCacheBehavior));
    }
}
