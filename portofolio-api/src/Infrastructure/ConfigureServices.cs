using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.DateTime;
using SemSnel.Portofolio.Infrastructure.Common.Files;
using SemSnel.Portofolio.Infrastructure.Common.Mapping;
using SemSnel.Portofolio.Infrastructure.Common.Mediatr;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers;
using SemSnel.Portofolio.Infrastructure.Common.Persistence;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Initialisers;
using SemSnel.Portofolio.Infrastructure.Common.Validations;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddWeatherForecastsServices(configuration);

        // add common services
        return services
            .AddDatabaseContext(configuration)
            .AddFileServices(configuration)
            .AddValidationServices(configuration)
            .AddMessageBroker(configuration)
            .AddDateTimeServices(configuration)
            .AddMediator(configuration)
            .AddMapping(configuration)
            .AddLocalization();
    }
}