using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;

/// <summary>
/// Abstraction for the database context that contains the message entities.
/// Message entities are entities that are used for Event sourcing and handling eventual consistency in the system.
/// </summary>
public interface IMessagesDatabaseContext : IDatabaseContext
{
    /// <summary>
    /// Gets the entity set for <see cref="DomainMessage"/> entities.
    /// </summary>
    DbSet<DomainMessage> DomainMessages { get; }

    /// <summary>
    /// Gets the entity set for <see cref="InboxMessage"/> entities.
    /// </summary>
    DbSet<InboxMessage> InboxMessages { get; }

    /// <summary>
    /// Gets the entity set for <see cref="OutboxMessage"/> entities.
    /// </summary>
    DbSet<OutboxMessage> OutboxMessages { get; }
}