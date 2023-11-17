using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// A repository that can be used to read entities.
/// </summary>
/// <typeparam name="TEntity"> The entity type. </typeparam>
/// <typeparam name="TId"> The id type. </typeparam>
public interface IWriteRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Adds the entity to the repository.
    /// </summary>
    /// <param name="entity"> The entity. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The created entity. </returns>
    public Task<ErrorOr<Created<TId>>> Add(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the entities to the repository.
    /// </summary>
    /// <param name="entities"> The entities. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The created entities. </returns>
    public Task<ErrorOr<Created>> AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the entity in the repository.
    /// </summary>
    /// <param name="id"> The id. </param>
    /// <param name="entity"> The entity. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The updated entity. </returns>
    public Task<ErrorOr<Updated<TId>>> Update(TId id, TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the entity in the repository.
    /// </summary>
    /// <param name="entity"> The entity. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The updated entity. </returns>
    public Task<ErrorOr<Updated<TId>>> Update(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// This method is used to update a range of entities.
    /// </summary>
    /// <param name="entities"> The entities. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The updated entities. </returns>
    public Task<ErrorOr<Updated>> UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity from the repository.
    /// </summary>
    /// <param name="id"> The id. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The deleted entity. </returns>
    public Task<ErrorOr<Deleted<TId>>> Delete(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity from the repository.
    /// </summary>
    /// <param name="entity"> The entity. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The deleted entity. </returns>
    public Task<ErrorOr<Deleted<TId>>> Delete(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entities from the repository.
    /// </summary>
    /// <param name="entities"> The entities. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The deleted entities. </returns>
    public Task<ErrorOr<Deleted>> DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}