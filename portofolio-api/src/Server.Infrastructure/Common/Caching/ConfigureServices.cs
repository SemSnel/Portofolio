using SemSnel.Portofolio.Server.Application.Common.Caching;

namespace SemSnel.Portofolio.Infrastructure.Common.Caching;

/// <summary>
/// Configures the caching services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the caching services to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the caching services added. </returns>
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        // add distributed caching
        services.AddDistributedMemoryCache();
        services.AddScoped<ICachingService, CachingService>();

        return services;
    }
}