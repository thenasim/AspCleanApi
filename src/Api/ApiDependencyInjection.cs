using Api.Common.Mappings;
using Api.Settings;
using Infrastructure.Common.Converters;
using Microsoft.AspNetCore.Http.Json;

namespace Api;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });
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
