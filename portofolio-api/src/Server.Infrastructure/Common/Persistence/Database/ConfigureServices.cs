using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

/// <summary>
/// Contains the extension methods for the <see cref="IServiceCollection"/> to add the database context.
/// </summary>
public static class ConfigureServices
{
    private const string MigrationAssembly = "SemSnel.Portofolio.Migrations.{0}";

    /// <summary>
    /// Adds the database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the database context added. </returns>
    /// <exception cref="NotSupportedException"> Thrown when the provider is not supported. </exception>
    public static IServiceCollection AddDatabaseContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // database settings
        services
            .AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddTransient<IInterceptor, AuditableEntityInterceptor>()
            .AddTransient<IInterceptor, DispatchDomainEventsInterceptor>();

        services
            .AddTransient<IUnitOfWork, UnitOfWork>();

        return services
            .AddScoped<IAppDbContextSeeder, AppDbContextSeeder>()
            .AddScoped<IAppContextInitialiser, AppContextInitialiser>()
            .AddDbContext<IAppDatabaseContext, AppDatabaseContext>((serviceProvider, options) =>
            {
                var databaseSettings = serviceProvider
                    .GetRequiredService<IOptions<DatabaseSettings>>()
                    .Value;

                var connectionString = databaseSettings.ConnectionString;
                var provider = databaseSettings.Provider;
                var migrationAssembly = string.Format(MigrationAssembly, provider);

                switch (provider)
                {
                    case "Sqlite":
                        options.UseSqlite(connectionString, builder =>
                        {
                            builder.MigrationsAssembly(migrationAssembly);
                        });
                        break;
                    default:
                        throw new NotSupportedException($"Provider {provider} is not supported");
                }
            });
    }
}