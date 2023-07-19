using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public static class ConfigureServices
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // add interceptors
        services
            .AddTransient<IInterceptor, AuditableEntityInterceptor>()
            .AddTransient<IInterceptor, DispatchDomainEventsInterceptor>();
        
        
        
        return services
            .AddTransient<IAppContextInitialiser, AppContextInitialiser>()
            .AddDbContext<IAppDatabaseContext, AppDatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("SemSnel.Portofolio");
            });
    }
}