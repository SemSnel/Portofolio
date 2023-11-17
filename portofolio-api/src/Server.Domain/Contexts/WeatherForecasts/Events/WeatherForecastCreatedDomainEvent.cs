using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Events;

public readonly record struct WeatherForecastCreatedDomainEvent(
    Guid Id,
    string? Summary,
    int TemperatureC
    ) : IDomainEvent;