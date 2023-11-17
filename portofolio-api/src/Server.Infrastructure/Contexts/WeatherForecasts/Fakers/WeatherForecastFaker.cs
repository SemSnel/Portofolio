using Bogus;
using SemSnel.Portofolio.Domain._Common.Enumerations;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Fakers;

/// <summary>
/// A faker for creating <see cref="WeatherForecast"/>s.
/// </summary>
public class WeatherForecastFaker : Faker<WeatherForecast>, IWeatherForecastFaker
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastFaker"/> class.
    /// </summary>
    public WeatherForecastFaker()
    {
        RuleFor(x => x.Date, f => f.Date.Past());
        RuleFor(x => x.TemperatureC, f => new Temperature(f.Random.Int(0, 40)));
        RuleFor(x => x.Summary, f => f.PickRandom(BaseEnumeration.GetAll<WeatherForecastSummary>()));
    }
}