using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.Common.Persistence;

public interface IWriteRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    public Task<ErrorOr<Created<TId>>> Add(TEntity entity, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Created>> AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    public Task<ErrorOr<Updated<TId>>> Update(TId id, TEntity entity, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Updated<TId>>> Update(TEntity entity, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Updated>> UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Deleted>> Delete(TId id, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Deleted>> Delete(TEntity entity, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<Deleted>> DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}