using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

/// <summary>
/// Abstract base class for the database context.
/// </summary>
/// <param name="options"> The <see cref="DbContextOptions{TContext}"/>. </param>
/// <param name="interceptors"> The <see cref="IEnumerable{T}"/> of <see cref="IInterceptor"/>. </param>
public abstract class BaseDatabaseContext(
        DbContextOptions<AppDatabaseContext> options,
        IEnumerable<IInterceptor> interceptors
    )
    : DbContext(options),
        IMessagesDatabaseContext,
        IIdempotencyDatabaseContext
{
    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="DomainMessage"/>.
    /// </summary>
    public DbSet<DomainMessage> DomainMessages => Set<DomainMessage>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="InboxMessage"/>.
    /// </summary>
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="OutboxMessage"/>.
    /// </summary>
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="IdempotentRequest"/>.
    /// </summary>
    public DbSet<IdempotentRequest> IdempotentRequests => Set<IdempotentRequest>();

    /// <inheritdoc/>
    public DbSet<TEntity> Set<TEntity, TId>()
        where TEntity : BaseEntity<TId>
        where TId : notnull
    {
        return Set<TEntity>();
    }

    /// <inheritdoc/>
    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return Database.MigrateAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(AppDatabaseContext).Assembly);
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .AddInterceptors(interceptors);
    }
}