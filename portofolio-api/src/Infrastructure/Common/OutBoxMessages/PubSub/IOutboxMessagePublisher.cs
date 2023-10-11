using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

public interface IOutboxMessagePublisher
{
    public Task<ErrorOr<Success>> Publish(OutBoxMessage message);
    
    public Task<ErrorOr<Success>> Publish(IEnumerable<OutBoxMessage> messages);
}