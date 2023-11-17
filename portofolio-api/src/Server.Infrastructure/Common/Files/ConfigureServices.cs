using SemSnel.Portofolio.Server.Application.Common.Files;

namespace SemSnel.Portofolio.Infrastructure.Common.Files;

/// <summary>
/// A static class for configuring the file services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the file services to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the file services added. </returns>
    public static IServiceCollection AddFileServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<ICsvService, CsvService>();
    }
}