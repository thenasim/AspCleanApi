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

        return services;
    }
}
