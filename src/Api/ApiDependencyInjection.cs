using Api.Common.Mappings;
using Api.Settings;

namespace Api;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument();
        services.AddSettings();
        services.AddMappings();

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        return services;
    }
}
