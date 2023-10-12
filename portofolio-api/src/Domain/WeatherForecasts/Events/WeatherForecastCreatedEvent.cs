using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.WeatherForecasts.Events;

public sealed class WeatherForecastCreatedEvent : EventBase
{
    public WeatherForecastCreatedEvent(Guid id, string summary, int temperatureC)
    {
        Id = id;
        Summary = summary;
        TemperatureC = temperatureC;
    }

    public Guid Id { get; }
    public string Summary { get; }
    public int TemperatureC { get; }
}