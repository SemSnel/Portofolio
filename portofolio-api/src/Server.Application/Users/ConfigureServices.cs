using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SemSnel.Portofolio.Server.Application.Users;

/// <summary>
/// Add the users to the service collection.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Add the users to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the configured users. </returns>
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<ICurrentIdentityService, FakeCurrentIdentityService>();
    }
}