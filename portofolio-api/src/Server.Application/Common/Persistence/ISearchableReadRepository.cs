using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// Searchable read repository.
/// </summary>
/// <typeparam name="TEntity"> The entity type. </typeparam>
/// <typeparam name="TId"> The id type. </typeparam>
public interface ISearchableReadRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// Searches for a single entity.
    /// </summary>
    /// <param name="query"> The query. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The entity. </returns>
    public Task<ErrorOr<TEntity>> SearchSingle(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for a single entity.
    /// </summary>
    /// <param name="query"> The query. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The entity. </returns>
    public Task<ErrorOr<IEnumerable<TEntity>>> Search(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for a single entity.
    /// </summary>
    /// <param name="query"> The query. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <typeparam name="TDestination"> The destination type. </typeparam>
    /// <returns> The entity. </returns>
    public Task<ErrorOr<IEnumerable<TDestination>>> Search<TDestination>(string query, CancellationToken cancellationToken = default);
}