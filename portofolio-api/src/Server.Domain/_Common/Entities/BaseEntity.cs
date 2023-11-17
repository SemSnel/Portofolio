using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;

namespace SemSnel.Portofolio.Domain._Common.Entities;

/// <summary>
/// Base class for entities.
/// </summary>
/// <typeparam name="TId"> The id type. </typeparam>
public abstract class BaseEntity<TId> :
    IEquatable<BaseEntity<TId>>,
    IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainNotifications = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity{TId}"/> class.
    /// </summary>
#pragma warning disable CS8618
    protected BaseEntity()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity{TId}"/> class.
    /// </summary>
    /// <param name="id"> The id. </param>
    protected BaseEntity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [JsonPropertyOrder(0)]
    public TId Id { get; protected set; }

    /// <inheritdoc/>
    [NotMapped]
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainNotifications.AsReadOnly();

    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is BaseEntity<TId> entity && Id.Equals(entity.Id);
    }

    /// <inheritdoc/>
    public bool Equals(BaseEntity<TId>? other)
    {
        return Equals((object?)other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    public void AddDomainEvent(IDomainEvent domainDomainEvent)
    {
        _domainNotifications.Add(domainDomainEvent);
    }

    /// <inheritdoc/>
    public void RemoveDomainEvent(IDomainEvent domainDomainEvent)
    {
        _domainNotifications.Remove(domainDomainEvent);
    }

    /// <inheritdoc/>
    public void ClearDomainEvents()
    {
        _domainNotifications.Clear();
    }

    /// <summary>
    /// Gets the string representation of the entity.
    /// </summary>
    /// <param name="obj"> The entity. </param>
    /// <returns> The string representation. </returns>
    public int GetHashCode(BaseEntity<TId> obj)
    {
        return HashCode.Combine(obj.Id, obj._domainNotifications);
    }
}
