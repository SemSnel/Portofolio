using SemSnel.Portofolio.Application.Common.Caching;

namespace SemSnel.Portofolio.Infrastructure.Common.Caching;

public static class ConfigureServices
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        // add distributed caching
        services.AddDistributedMemoryCache();
        services.AddScoped<ICachingService, CachingService>();

        return services;
    }
}