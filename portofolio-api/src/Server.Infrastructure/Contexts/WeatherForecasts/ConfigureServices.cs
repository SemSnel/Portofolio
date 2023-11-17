using SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Fakers;
using SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Persistence;
using SemSnel.Portofolio.Server.Application.WeatherForecasts.Repositories;

namespace SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts;

/// <summary>
/// Class for configuring the services for the weather forecasts context.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Configures the services for the weather forecasts context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/>. </param>
    /// <returns> The <see cref="IServiceCollection"/> with the added services. </returns>
    public static IServiceCollection AddWeatherForecastsServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddTransient<IWeatherForecastFaker, WeatherForecastFaker>()
            .AddTransient<IWeatherForecastsRepository, WeatherForecastsRepository>();
    }
}