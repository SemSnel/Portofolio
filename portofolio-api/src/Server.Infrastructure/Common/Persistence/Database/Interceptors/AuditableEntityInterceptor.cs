using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain._Common.Entities.Auditability;
using SemSnel.Portofolio.Server.Application.Common.DateTime;
using SemSnel.Portofolio.Server.Application.Users;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;

/// <summary>
/// An interceptor for the <see cref="DbContext.SaveChanges()"/> and <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> methods.
/// When saving changes, this interceptor will update the <see cref="IAuditableEntity"/>s.
/// </summary>
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentIdentityService _currentIdentityService;
    private readonly IDateTimeProvider _dateTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntityInterceptor"/> class.
    /// </summary>
    /// <param name="currentIdentityService"> The <see cref="ICurrentIdentityService"/>. </param>
    /// <param name="dateTime"> The <see cref="IDateTimeProvider"/>. </param>
    public AuditableEntityInterceptor(
        ICurrentIdentityService currentIdentityService,
        IDateTimeProvider dateTime)
    {
        _currentIdentityService = currentIdentityService;
        _dateTime = dateTime;
    }

    /// <inheritdoc/>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc/>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentIdentityService.Id;
                entry.Entity.CreatedOn = _dateTime.Now();
            }

            if (entry.State != EntityState.Added && entry.State != EntityState.Modified &&
                !entry.HasChangedOwnedEntities())
            {
                continue;
            }

            entry.Entity.LastModifiedBy = _currentIdentityService.Id;
            entry.Entity.LastModifiedOn = _dateTime.Now();
        }
    }
}