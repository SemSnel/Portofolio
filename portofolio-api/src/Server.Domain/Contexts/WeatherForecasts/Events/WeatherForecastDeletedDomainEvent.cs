using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Events;

/// <summary>
/// An event that is triggered when a weather forecast is deleted.
/// </summary>
public record WeatherForecastDeletedDomainEvent(Guid Id) : IDomainEvent;