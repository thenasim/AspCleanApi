﻿using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Infrastructure.Common.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Settings;
using Infrastructure.Settings.Database;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using Domain.Enums;

namespace Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopment)
    {
        services.AddSettings();

        services.AddSingleton<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContextPool<ApplicationDbContext>((provider, optionsBuilder) =>
        {
            var dbOptions = provider.GetService<IOptions<DatabaseSettings>>()?.Value ??
                            throw new Exception($"{nameof(DatabaseSettings)} could not be loaded.");
            var auditableEntitySaveChangesInterceptor = provider.GetRequiredService<AuditableEntitySaveChangesInterceptor>();
            var dataSource = ConfigureNpgsqlDataSource(dbOptions.ConnectionString!);

            optionsBuilder.UseNpgsql(dataSource, serverAction =>
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
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseExceptionProcessor();

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

    private static NpgsqlDataSource ConfigureNpgsqlDataSource(string connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString!);

        // Map enums
        dataSourceBuilder.MapEnum<Gender>();

        return dataSourceBuilder.Build();
    }
}
