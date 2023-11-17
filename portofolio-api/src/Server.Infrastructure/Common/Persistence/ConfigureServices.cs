using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Server.Application.Common.Persistence;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence;

/// <summary>
/// Configures the services for the persistence layer.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds the <see cref="IAppDatabaseContext"/> to the service collection.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/>. </param>
    /// <typeparam name="TEntity"> The entity type. </typeparam>
    /// <typeparam name="TId"> The entity id type. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> with the <see cref="IAppDatabaseContext"/> added. </returns>
    public static IServiceCollection AddRepository<TEntity, TId>(this IServiceCollection services)
        where TEntity : BaseEntity<TId>
        where TId : notnull
    {
        return services
            .AddScoped<ISearchableReadRepository<TEntity, TId>, Repository<TEntity, TId>>()
            .AddScoped<IReadRepository<TEntity, TId>, Repository<TEntity, TId>>()
            .AddScoped<IWriteRepository<TEntity, TId>, Repository<TEntity, TId>>();
    }
}