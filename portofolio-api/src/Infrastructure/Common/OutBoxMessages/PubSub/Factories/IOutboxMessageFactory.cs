using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Factories;

public interface IOutboxMessageFactory
{
    public OutBoxMessage Create(EventBase domainEvent);
}