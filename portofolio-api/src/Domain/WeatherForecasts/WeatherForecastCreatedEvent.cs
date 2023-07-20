using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.WeatherForecasts;

public sealed class WeatherForecastCreatedEvent : EventBase
{
    public WeatherForecastCreatedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}