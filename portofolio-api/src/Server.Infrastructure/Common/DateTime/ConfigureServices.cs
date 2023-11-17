using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.DateTime;

/// <summary>
/// A class that configures the services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the date time services to the services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <param name="configuration"> The configuration. </param>
    /// <returns> The services with the date time services added. </returns>
    public static IServiceCollection AddDateTimeServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}