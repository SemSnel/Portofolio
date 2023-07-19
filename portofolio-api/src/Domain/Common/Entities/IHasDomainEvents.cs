namespace SemSnel.Portofolio.Domain.Common.Entities;

/// <summary>
/// Interface for entities that have domain events.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gives access to the domain events.
    /// </summary>
    IReadOnlyCollection<EventBase> DomainEvents { get; }
    
    /// <summary>
    /// Adds a domain event.
    /// </summary>
    /// <param name="domainEvent">Event to add.</param>
    void AddDomainEvent(EventBase domainEvent);
    
    /// <summary>
    /// Removes a domain event.
    /// </summary>
    /// <param name="domainEvent">Event to remove.</param>
    void RemoveDomainEvent(EventBase domainEvent);
    
    /// <summary>
    /// Removes all domain events.
    /// </summary>
    void ClearDomainEvents();
}
