using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Fakers;

/// <summary>
/// A faker for creating <see cref="WeatherForecast"/>s.
/// </summary>
public interface IWeatherForecastFaker
{
    /// <summary>
    /// Generates a list of <see cref="WeatherForecast"/>s.
    /// </summary>
    /// <param name="count"> The count. </param>
    /// <param name="ruleSets"> The rule sets. </param>
    /// <returns> The <see cref="IEnumerable{T}"/> of <see cref="WeatherForecast"/>. </returns>
    List<WeatherForecast> Generate(int count, string ruleSets);
}