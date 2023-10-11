using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Factories;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Database.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IOutboxMessageFactory _outboxMessageFactory;

    public DispatchDomainEventsInterceptor(IOutboxMessageFactory outboxMessageFactory)
    {
        _outboxMessageFactory = outboxMessageFactory;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);

    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) 
            return Task.CompletedTask;

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
                domainEvent => _outboxMessageFactory.Create(domainEvent)
                )
            .ToArray();
        
        context.Set<OutBoxMessage>().AddRange(outboxMessages);
        
        return Task.CompletedTask;
    }
}