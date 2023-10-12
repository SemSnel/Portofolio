using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.WeatherForecasts;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public class AppDatabaseContext : DbContext, IAppDatabaseContext
{
    private readonly IEnumerable<IInterceptor> _interceptors;
    
    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options, IEnumerable<IInterceptor> interceptors) : base(options)
    {
        _interceptors = interceptors;
    }
    
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
    public DbSet<OutBoxMessage> OutboxMessages => Set<OutBoxMessage>();
    public DbSet<IdempotentRequest> IdempotentRequests => Set<IdempotentRequest>();

    public DbSet<TEntity> Set<TEntity, TId>() where TEntity : Entity<TId> where TId : notnull
    {
        return Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .AddInterceptors(_interceptors);
    }
    
    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Database.MigrateAsync(cancellationToken);
    }
}

