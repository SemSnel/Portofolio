namespace SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;

/// <summary>
/// Interface for entities that have domain events.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Adds a domain event.
    /// </summary>
    /// <param name="domainDomainEvent">Event to add.</param>
    void AddDomainEvent(IDomainEvent domainDomainEvent);

    /// <summary>
    /// Removes a domain event.
    /// </summary>
    /// <param name="domainDomainEvent">Event to remove.</param>
    void RemoveDomainEvent(IDomainEvent domainDomainEvent);

    /// <summary>
    /// Removes all domain events.
    /// </summary>
    void ClearDomainEvents();
}
