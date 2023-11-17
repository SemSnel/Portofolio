namespace SemSnel.Portofolio.Domain._Common.Entities;

/// <summary>
/// Base class for aggregate roots.
/// Aggregate roots are entities that are the root of an aggregate.
/// They are the only entities that can be added to the repository.
/// Changes to aggregate roots are persisted to the database.
/// </summary>
/// <typeparam name="TId"> The id type. </typeparam>
public abstract class BaseAggregateRoot<TId>
    : BaseEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseAggregateRoot{TId}"/> class.
    /// </summary>
    /// <param name="id"> The id. </param>
    protected BaseAggregateRoot(TId id)
        : base(id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseAggregateRoot{TId}"/> class.
    /// </summary>
#pragma warning disable CS8618
    protected BaseAggregateRoot()
    {
    }
#pragma warning restore CS8618
}
