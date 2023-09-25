using SemSnel.Portofolio.Domain.Common.Entities;

namespace SemSnel.Portofolio.Domain.WeatherForecasts.Events;

public class WeatherForecastDeletedEvent : EventBase
{
    public WeatherForecastDeletedEvent(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}