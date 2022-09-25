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
        
        // Configure Entity Framework
        services.AddDbContextPool<ApplicationDbContext>((provider, optionsBuilder) =>
        {
            var dbOptions = provider.GetService<IOptions<DatabaseSettings>>()?.Value ??
                            throw new Exception($"{nameof(DatabaseSettings)} could not be loaded.");

            optionsBuilder.UseNpgsql(dbOptions.ConnectionString!, serverAction =>
            {
                serverAction.EnableRetryOnFailure(dbOptions.MaxRetryOnFailure);
                serverAction.CommandTimeout(dbOptions.CommandTimeout);
                serverAction.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });
            optionsBuilder.AddInterceptors(new AuditableEntitySaveChangesInterceptor());
            
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
