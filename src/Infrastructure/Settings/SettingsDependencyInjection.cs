using Infrastructure.Settings.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Settings;

internal static class SettingsDependencyInjection
{
    internal static IServiceCollection AddSettings(this IServiceCollection services)
    {
        // Database
        services.AddSingleton<IValidateOptions<DatabaseSettings>, ValidateDatabaseSettings>();
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.SectionName)
            .ValidateOnStart();
        services.AddOptions<DatabaseSettings>();

        return services;
    }
}
