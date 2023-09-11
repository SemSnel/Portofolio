using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts.Fakers;

public interface IWeatherForecastFaker
{
    List<WeatherForecast> Generate(int count, string ruleSets);
}