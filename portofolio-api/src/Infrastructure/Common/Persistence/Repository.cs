using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Application.WeatherForecasts;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

public abstract class Repository<TEntity, TId> : 
    IReadRepository<TEntity, TId>,
    ISearchableReadRepository<TEntity, TId>,
    IWriteRepository<TEntity, TId> 
    where TEntity : Entity<TId>
    where TId : notnull
{
    private readonly IAppDatabaseContext _context;
    private readonly IMapper _mapper;
    
    protected Repository(IAppDatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IQueryable<TEntity> Get()
    {
        return _context.Set<TEntity, TId>();
    }

    public IQueryable<T> Get<T>(Expression<Func<TEntity, T>> selector)
    {
        return _context.Set<TEntity, TId>().Select(selector);
    }

    public async Task<ErrorOr<int>> Count(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity, TId>().CountAsync(cancellationToken);
    }

    public async Task<ErrorOr<TEntity>> GetById(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await _context
            .Set<TEntity, TId>()
            .SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
        
        if (entity is null)
        {
            return Error.NotFound();
        }
        
        return entity;
    }

    public async Task<ErrorOr<TEntity>> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity, TId>()
            .SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public ErrorOr<Created<TId>> Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _context.Set<TEntity, TId>().Add(entity);

        if (entry is null)
        {
            return Error.Failure("Failed to add entity to database");
        }

        return Result.Created(entry.Entity.Id);
    }

    public ErrorOr<Created> AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity, TId>().AddRange(entities);
        
        return Result.Created();
    }

    public ErrorOr<Updated<TId>> Update(TId id, TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _context.Set<TEntity, TId>().Update(entity);
        
        if (entry is null)
        {
            return Error.NotFound();
        }
        
        return Result.Updated<TId>(entry.Entity.Id);
    }

    public ErrorOr<Updated<TId>> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context
            .Set<TEntity, TId>()
            .Update(entity);
        
        return Result.Updated<TId>(entity.Id);
    }

    public ErrorOr<Updated> UpdateRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context
            .Set<TEntity, TId>()
            .UpdateRange(entities);
        
        return Result.Updated();
    }

    public ErrorOr<Deleted> Delete(TId id, CancellationToken cancellationToken = default)
    {
        var entity = _context.Set<TEntity, TId>().Find(id);
        
        if (entity is null)
        {
            return Error.NotFound();
        }
        
        _context.Set<TEntity, TId>().Remove(entity);
        
        return Result.Deleted();
    }

    public ErrorOr<Deleted> Delete(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = _context.Set<TEntity, TId>().Remove(entity);
        
        if (entry is null)
        {
            return Error.NotFound();
        }
        
        return Result.Deleted();
    }

    public ErrorOr<Deleted> DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context
            .Set<TEntity, TId>()
            .RemoveRange(entities);
        
        return Result.Deleted();
    }

    public async Task<ErrorOr<TEntity>> SearchSingle(string query, CancellationToken cancellationToken = default)
    {
        var entity = await _context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            return Error.NotFound();
        }
        
        return entity;
    }

    public async Task<ErrorOr<IEnumerable<TEntity>>> Search(string query, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .ToListAsync(cancellationToken);
    }

    public async Task<ErrorOr<IEnumerable<TDestination>>> Search<TDestination>(string query,
        CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity, TId>()
            .FromSqlRaw(query)
            .ProjectTo<TDestination>(_mapper)
            .ToListAsync(cancellationToken);
    }
}