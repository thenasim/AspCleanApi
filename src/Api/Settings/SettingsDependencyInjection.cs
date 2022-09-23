using Api.Settings.Database;
using Microsoft.Extensions.Options;

namespace Api.Settings;

internal static class SettingsDependencyInjection
{
    internal static IServiceCollection AddSettings(this IServiceCollection services)
    {
        // Database
        services.AddSingleton<IValidateOptions<DatabaseSettings>, ValidateDatabaseSettings>();
        services.AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.SectionName)
            .ValidateOnStart();

        return services;
    }
}
