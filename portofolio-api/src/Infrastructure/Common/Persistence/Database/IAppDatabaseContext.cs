using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public interface IAppDatabaseContext
{
    DbSet<WeatherForecast> WeatherForecasts { get; }
    DbSet<TEntity> Set<TEntity, TId>() where TEntity : Entity<TId> where TId : notnull;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}