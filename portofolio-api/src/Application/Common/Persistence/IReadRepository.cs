using System.Linq.Expressions;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Persistence;

public interface IReadRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
{
    public IQueryable<TEntity> Get();
    
    public Task<ErrorOr<int>> Count(CancellationToken cancellationToken = default);

    public Task<ErrorOr<TEntity>> GetById(TId id, CancellationToken cancellationToken = default);
    
    public Task<ErrorOr<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}