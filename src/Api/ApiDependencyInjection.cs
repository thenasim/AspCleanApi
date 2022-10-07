using Api.Common.Mappings;
using Api.Settings;
using Infrastructure.Common.Converters;

namespace Api;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddHealthChecks();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument(configure =>
        {
            configure.Title = "Clean Architecture API";
            configure.Version = "1.0.0";
        });
        services.AddSettings();
        services.AddMappings();

        return services;
    }
}
