using SemSnel.Portofolio.Infrastructure.Common.Caching;
using SemSnel.Portofolio.Infrastructure.Common.DateTime;
using SemSnel.Portofolio.Infrastructure.Common.Files;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency;
using SemSnel.Portofolio.Infrastructure.Common.Logging;
using SemSnel.Portofolio.Infrastructure.Common.Mapping;
using SemSnel.Portofolio.Infrastructure.Common.Mediatr;
using SemSnel.Portofolio.Infrastructure.Common.Messages;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;
using SemSnel.Portofolio.Infrastructure.Common.Validations;
using SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure;

/// <summary>
/// Configures the infrastructure services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the infrastructure services to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the infrastructure services added. </returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddWeatherForecastsServices(configuration);

        // add common services
        return services
            .AddDatabaseContext(configuration)
            .AddMessageContext()
            .AddLogging(configuration)
            .AddIdempotency(configuration)
            .AddCaching(configuration)
            .AddFileServices(configuration)
            .AddValidationServices(configuration)
            .AddDomainMessagesServices(configuration)
            .AddDateTimeServices(configuration)
            .AddMediator(configuration)
            .AddMapping(configuration)
            .AddLocalization();
    }
}