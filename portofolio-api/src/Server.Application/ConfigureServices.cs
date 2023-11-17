using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SemSnel.Portofolio.Server.Application.Common.Authorisations;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Server.Application;

/// <summary>
/// A class that configures the services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the application to the services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <param name="configuration"> The configuration. </param>
    /// <returns> The services with the application added. </returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuthorisations()
            .AddUsers(configuration);
    }
}