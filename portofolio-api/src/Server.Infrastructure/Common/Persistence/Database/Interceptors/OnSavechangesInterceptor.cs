using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;

/// <summary>
/// An interceptor for the <see cref="DbContext.SaveChanges()"/> method.
/// </summary>
public static class OnSaveChangesInterceptorExtensions
{
    /// <summary>
    /// Checks if the <see cref="EntityEntry"/> has any owned entities that have been changed.
    /// </summary>
    /// <param name="entry"> The <see cref="EntityEntry"/>. </param>
    /// <returns> True if the <see cref="EntityEntry"/> has any owned entities that have been changed, otherwise false. </returns>
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);
}