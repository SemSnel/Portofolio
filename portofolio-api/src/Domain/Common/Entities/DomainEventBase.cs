namespace SemSnel.Portofolio.Domain.Common.Entities;

/// <summary>
/// Is the base class for domain events.
/// Products.Domain events are events that are raised by the domain.
/// Other layers should not raise domain events.
/// The domain events are used to notify other layers of changes in the domain.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that is the source of the event.</typeparam>
public abstract class DomainEventBase<TEntity> : EventBase
{
    public TEntity Entity { get; }
    
    protected DomainEventBase(TEntity entity)
    {
        Entity = entity;
    }
}
/// <summary>
/// Is the base class for domain events.
/// Products.Domain events are events that are raised by the domain.
/// Other layers should not raise domain events.
/// The domain events are used to notify other layers of changes in the domain.
/// </summary>
public abstract class DomainEventBase : EventBase
{
}
