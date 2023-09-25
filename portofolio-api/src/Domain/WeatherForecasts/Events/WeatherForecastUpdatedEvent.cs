using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.WeatherForecasts;

public sealed class WeatherForecastUpdatedEvent : EventBase
{
    public WeatherForecastUpdatedEvent(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}