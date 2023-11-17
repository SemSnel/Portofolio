using Ardalis.Specification;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Specifications;

/// <summary>
/// Gets all weather forecasts with extreme temperatures.
/// </summary>
public sealed class GetExtremeTemperature : Specification<WeatherForecast>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetExtremeTemperature"/> class.
    /// </summary>
    public GetExtremeTemperature()
    {
        Query.OrderBy(x => x.TemperatureC);
        Query.Where(x => x.TemperatureC > 30 || x.TemperatureC < 10);
        Query.Where(x => x.Summary == WeatherForecastSummary.Sunny);
    }
}