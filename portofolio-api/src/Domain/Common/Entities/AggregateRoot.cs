namespace SemSnel.Portofolio.Domain.Common.Entities;

/// <summary>
/// Base class for aggregate roots.
/// Aggregate roots are entities that are the root of an aggregate.
/// They are the only entities that can be added to the repository.
/// Changes to aggregate roots are persisted to the database.
/// </summary>
/// <typeparam name="TId"></typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }

#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
}
