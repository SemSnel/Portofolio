using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Persistence;

public interface ISearchableReadRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    public Task<ErrorOr<TEntity>> SearchSingle(string query, CancellationToken cancellationToken = default);
    public Task<ErrorOr<IEnumerable<TEntity>>> Search(string query, CancellationToken cancellationToken = default);
    public Task<ErrorOr<IEnumerable<TDestination>>> Search<TDestination>(string query, CancellationToken cancellationToken = default);
}