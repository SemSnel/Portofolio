using System.Linq.Expressions;
using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// A read repository.
/// </summary>
/// <typeparam name="TEntity"> The entity type. </typeparam>
/// <typeparam name="TId"> The id type. </typeparam>
public interface IReadRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Gets an instance of <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <returns> The entities. </returns>
    public IQueryable<TEntity> Get();

    /// <summary>
    /// Gets an instance of <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <param name="selector"> The selector. </param>
    /// <typeparam name="T"> The type. </typeparam>
    /// <returns> The entities. </returns>
    public IQueryable<T> Get<T>(Expression<Func<TEntity, T>> selector);

    /// <summary>
    /// Gets an instance of <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The entities. </returns>
    public Task<ErrorOr<int>> Count(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an entity by id.
    /// </summary>
    /// <param name="id"> The id. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The entity. </returns>
    public Task<ErrorOr<TEntity>> GetById(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an entity by id.
    /// </summary>
    /// <param name="predicate"> The predicate. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The entity. </returns>
    public Task<ErrorOr<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}