using SemSnel.Portofolio.Application.Common.Persistence;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Mapping;
using SemSnel.Portofolio.Infrastructure.Common.Mediatr;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddRepository<TEntity, TId>(this IServiceCollection services) 
        where TEntity : Entity<TId> 
        where TId : notnull
    {
        return services
            .AddScoped<ISearchableReadRepository<TEntity, TId>, Repository<TEntity, TId>>()
            .AddScoped<IReadRepository<TEntity, TId>, Repository<TEntity, TId>>()
            .AddScoped<IWriteRepository<TEntity, TId>, Repository<TEntity, TId>>();
    }
}