using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

/// <summary>
/// Base repository for the <see cref="AppDbContext"/>.
/// </summary>
/// <typeparam name="TEntity"> The entity type. </typeparam>
/// <typeparam name="TId"> The entity id type. </typeparam>
public abstract class Repository<TEntity, TId>(
    IAppDatabaseContext context,
    IMapper mapper) :
    IReadRepository<TEntity, TId>,
    ISearchableReadRepository<TEntity, TId>,
    IWriteRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
    where TId : notnull
{
    /// <inheritdoc/>
    public IQueryable<TEntity> Get()
    {
        return context.Set<TEntity, TId>();
    }

    /// <inheritdoc/>
    public IQueryable<T> Get<T>(Expression<Func<TEntity, T>> selector)
    {
        return context.Set<TEntity, TId>().Select(selector);
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<int>> Count(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity, TId>().CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<TEntity>> GetById(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await context
            .Set<TEntity, TId>()
            .SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

        if (entity is null)
        {
            return Error.NotFound();
        }

        return entity;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await context
            .Set<TEntity, TId>()
            .SingleOrDefaultAsync(predicate, cancellationToken);

        return result ?? ErrorOr.From<TEntity>(Error.NotFound());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Created<TId>>> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Set<TEntity, TId>().Add(entity);

        if (entry is null)
        {
            return Task.FromResult<ErrorOr<Created<TId>>>(Error.Failure("Failed to add entity to database"));
        }

        return Task.FromResult<ErrorOr<Created<TId>>>(entry.Entity.Id.ToCreated());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Created>> AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        context.Set<TEntity, TId>().AddRange(entities);

        return Task.FromResult<ErrorOr<Created>>(Result.Created);
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Updated<TId>>> Update(TId id, TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Set<TEntity, TId>().Update(entity);

        if (entry is null)
        {
            return Task.FromResult<ErrorOr<Updated<TId>>>(Error.NotFound());
        }

        return Task.FromResult<ErrorOr<Updated<TId>>>(entity.Id.ToUpdated());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Updated<TId>>> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        context
            .Set<TEntity, TId>()
            .Update(entity);

        return Task.FromResult<ErrorOr<Updated<TId>>>(entity.Id.ToUpdated());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Updated>> UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        context
            .Set<TEntity, TId>()
            .UpdateRange(entities);

        return Task.FromResult<ErrorOr<Updated>>(Result.Updated);
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Deleted<TId>>> Delete(TId id, CancellationToken cancellationToken = default)
    {
        var entity = context.Set<TEntity, TId>().Find(id);

        if (entity is null)
        {
            return Task.FromResult<ErrorOr<Deleted<TId>>>(Error.NotFound());
        }

        context.Set<TEntity, TId>().Remove(entity);

        return Task.FromResult<ErrorOr<Deleted<TId>>>(entity.Id.ToDeleted());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Deleted<TId>>> Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Set<TEntity, TId>().Remove(entity);

        if (entry is null)
        {
            return Task.FromResult<ErrorOr<Deleted<TId>>>(Error.NotFound());
        }

        return Task.FromResult<ErrorOr<Deleted<TId>>>(entity.Id.ToDeleted());
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Deleted>> DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        context
            .Set<TEntity, TId>()
            .RemoveRange(entities);

        return Task.FromResult<ErrorOr<Deleted>>(Result.Deleted);
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<TEntity>> SearchSingle(string query, CancellationToken cancellationToken = default)
    {
        var entity = await context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            return Error.NotFound();
        }

        return entity;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IEnumerable<TEntity>>> Search(string query, CancellationToken cancellationToken = default)
    {
        return await context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<IEnumerable<TDestination>>> Search<TDestination>(
        string query,
        CancellationToken cancellationToken = default)
    {
        return await context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .ProjectTo<TDestination>(mapper)
            .ToListAsync(cancellationToken);
    }
}