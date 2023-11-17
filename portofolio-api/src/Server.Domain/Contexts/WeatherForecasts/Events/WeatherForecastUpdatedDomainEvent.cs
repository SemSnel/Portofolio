using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;

namespace SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.Events;

public sealed record WeatherForecastUpdatedDomainEvent(Guid Id) : IDomainEvent
{
}