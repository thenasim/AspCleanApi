using Api.Settings;

namespace Api;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocument();
        services.AddSettings();

        return services;
    }
}
