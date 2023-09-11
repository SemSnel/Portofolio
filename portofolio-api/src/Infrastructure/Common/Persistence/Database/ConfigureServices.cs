using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Seeders;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public static class ConfigureServices
{
    public const string MigrationAssembly = "SemSnel.Portofolio.Migrations.{0}";
    
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // database settings
        services
            .AddOptions<DatabaseSettings>()
            .BindConfiguration(DatabaseSettings.Section)
            .ValidateDataAnnotations();
        
        // add interceptors
        services
            .AddTransient<IInterceptor, AuditableEntityInterceptor>()
            .AddTransient<IInterceptor, DispatchDomainEventsInterceptor>();

        return services
            .AddScoped<IAppDbContextSeeder, AppDbContextSeeder>()
            .AddScoped<IAppContextInitialiser, AppContextInitialiser>()
            .AddDbContext<IAppDatabaseContext, AppDatabaseContext>(options =>
            {
                var databaseSettings = configuration
                                           .GetSection(DatabaseSettings.Section)
                                           .Get<DatabaseSettings>() ?? throw new ArgumentNullException("No database settings found");
                
                var connectionString = databaseSettings.ConnectionString;
                var provider = databaseSettings.Provider;
                var migrationAssembly = string.Format(MigrationAssembly, provider);
                
                // add database based on provider
                
                switch (provider)
                {
                    case "Sqlite":
                        options.UseSqlite(connectionString, builder =>
                        {
                            builder.MigrationsAssembly(migrationAssembly);
                        });
                        break;
                    case "InMemory":
                        options.UseInMemoryDatabase(connectionString);
                        break;
                    case "SqlServer":
                        options.UseSqlServer(connectionString, builder =>
                        {
                            builder.MigrationsAssembly(migrationAssembly);
                        });
                        break;
                }
            });
    }
}