using SemSnel.Portofolio.Application.WeatherForecasts.Repositories;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts.Fakers;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts.Orders;
using SemSnel.Portofolio.Infrastructure.WeatherForecasts.Persistence;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts;

public static class ConfigureServices
{
    public static IServiceCollection AddWeatherForecastsServices(this IServiceCollection services, IConfiguration configuration)
    {
        // order services
        services
            .AddTransient<IOrderRepository, OrderRepository>();
        
        return services
            .AddTransient<IWeatherForecastFaker, WeatherForecastFaker>()
            .AddTransient<IWeatherForecastsRepository, WeatherForecastsRepository>();
    }
}