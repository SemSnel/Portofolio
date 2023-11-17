using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain._Common.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

/// <summary>
/// A base interface for a database context.
/// </summary>
public interface IDatabaseContext
{
    /// <summary>
    /// A set of the entities.
    /// </summary>
    /// <typeparam name="TEntity"> The type of the entity. </typeparam>
    /// <typeparam name="TId"> The type of the entity's identifier. </typeparam>
    /// <returns> The <see cref="DbSet{TEntity}"/>. </returns>
    DbSet<TEntity> Set<TEntity, TId>()
        where TEntity : BaseEntity<TId>
        where TId : notnull;

    /// <summary>
    /// Saves the changes to the database.
    /// </summary>
    /// <param name="cancellationToken"> The <see cref="CancellationToken"/>. </param>
    /// <returns> The number of affected rows. </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Migrates the database.
    /// </summary>
    /// <param name="cancellationToken"> The <see cref="CancellationToken"/>. </param>
    /// <returns> The <see cref="Task"/>. </returns>
    Task MigrateAsync(CancellationToken cancellationToken = default);
}