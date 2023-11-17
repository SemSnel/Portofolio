using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

public interface IIdempotencyDatabaseContext : IDatabaseContext
{
    DbSet<IdempotentRequest> IdempotentRequests { get; }
}