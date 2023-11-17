using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Factories;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;

/// <summary>
/// Dispatches the domain events.
/// </summary>
public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IDomainMessageFactory _domainMessageFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DispatchDomainEventsInterceptor"/> class.
    /// </summary>
    /// <param name="domainMessageFactory"> The <see cref="IDomainMessageFactory"/>. </param>
    public DispatchDomainEventsInterceptor(IDomainMessageFactory domainMessageFactory)
    {
        _domainMessageFactory = domainMessageFactory;
    }

    /// <inheritdoc/>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc/>
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null)
        {
            return Task.CompletedTask;
        }

        var entities = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToArray();

        var domainEvents = entities
            .SelectMany(entity =>
            {
                var events = entity
                    .DomainEvents
                    .ToArray();

                entity.ClearDomainEvents();

                return events;
            })
            .ToArray();

        var outboxMessages = domainEvents
            .Select(
                domainEvent => _domainMessageFactory.Create(domainEvent))
            .ToArray();

        context.Set<DomainMessage>().AddRange(outboxMessages);

        return Task.CompletedTask;
    }
}