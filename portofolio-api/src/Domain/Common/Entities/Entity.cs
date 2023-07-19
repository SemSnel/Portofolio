using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain.Common.Entities;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IHasDomainEvents
    where TId : notnull
{
    [JsonPropertyOrder(0)]
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    private readonly List<EventBase> _domainNotifications = new();

    [NotMapped]
    [JsonIgnore]
    public IReadOnlyCollection<EventBase> DomainEvents => _domainNotifications.AsReadOnly();
    
    public void AddDomainEvent(EventBase domainEvent)
    {
        _domainNotifications.Add(domainEvent);
    }

    public void RemoveDomainEvent(EventBase domainEvent)
    {
        _domainNotifications.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainNotifications.Clear();
    }

#pragma warning disable CS8618
    protected Entity()
    {
    }
#pragma warning restore CS8618
}
