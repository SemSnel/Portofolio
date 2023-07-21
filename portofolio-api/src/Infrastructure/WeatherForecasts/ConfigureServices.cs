using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts.Persistence;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts;

public static class ConfigureServices
{
    public static IServiceCollection AddWeatherForecastsServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddTransient<IWeatherForecastsRepository, WeatherForecastsRepository>();
    }
}