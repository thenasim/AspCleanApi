using Application.Common.Interfaces;
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
        services.AddDbContext<ApplicationDbContext>((provider, optionsBuilder) =>
        {
            var dbOptions = provider.GetService<IOptions<DatabaseSettings>>()?.Value ??
                            throw new Exception($"{nameof(DatabaseSettings)} could not be loaded.");

            optionsBuilder.UseNpgsql(dbOptions.ConnectionString!, serverAction =>
            {
                serverAction.EnableRetryOnFailure(dbOptions.MaxRetryOnFailure);
                serverAction.CommandTimeout(dbOptions.CommandTimeout);
                serverAction.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                serverAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

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
            optionsBuilder.AddInterceptors(new AuditableEntitySaveChangesInterceptor());
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (isDevelopment)
            {
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging();
            }
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
