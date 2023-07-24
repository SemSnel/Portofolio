using Bogus;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts.Fakers;

public class WeatherForecastFaker : Faker<WeatherForecast>, IWeatherForecastFaker
{
    public WeatherForecastFaker()
    {
        RuleFor(x => x.Date, f => f.Date.PastDateOnly());
        RuleFor(x => x.TemperatureC, f => f.Random.Int(0, 40));
        RuleFor(x => x.Summary, f => f.Lorem.Sentence());
    }
}