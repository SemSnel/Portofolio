using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public interface IAppDatabaseContext
{
    DbSet<WeatherForecast> WeatherForecasts { get; }
    DbSet<OutBoxMessage> OutboxMessages { get; }
    DbSet<IdempotentRequest> IdempotentRequests { get; }
    
    DbSet<TEntity> Set<TEntity, TId>() where TEntity : Entity<TId> where TId : notnull;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task MigrateAsync(CancellationToken cancellationToken = default);
}