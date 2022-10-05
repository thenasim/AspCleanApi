using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Infrastructure.Common.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Settings;
using Infrastructure.Settings.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopment)
    {
        services.AddSettings();

        // Configure Entity Framework // TODO: AddDbContextPool (But dependency injection will not work in AppDbContext because context pool works like singleton)
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<ApplicationDbContext>((provider, optionsBuilder) =>
        {
            var dbOptions = provider.GetService<IOptions<DatabaseSettings>>()?.Value ??
                            throw new Exception($"{nameof(DatabaseSettings)} could not be loaded.");
            var auditableEntitySaveChangesInterceptor = provider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();

            optionsBuilder.UseNpgsql(dbOptions.ConnectionString!, serverAction =>
            {
                serverAction.EnableRetryOnFailure(dbOptions.MaxRetryOnFailure);
                serverAction.CommandTimeout(dbOptions.CommandTimeout);
                serverAction.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);

                // Set db version
                if (string.IsNullOrEmpty(dbOptions.DatabaseVersion) == false)
                {
                    if (Version.TryParse(dbOptions.DatabaseVersion, out var dbVersion) == false)
                    {
                        throw new Exception("Failed to parse DatabaseVersion provided in appsettings.json");
                    }
                    serverAction.SetPostgresVersion(dbVersion);
                }
            });
            optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (isDevelopment)
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
            }
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        // Common services
        services.AddTransient<IDateTimeService, DateTimeService>();

        return services;
    }
}
