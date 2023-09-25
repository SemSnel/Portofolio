using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.Common.MessageBrokers;

public interface IMessagePublisher
{
    Task<ErrorOr<Success>> Publish(IMessage message);
    
    Task<ErrorOr<Success>> Publish(IEnumerable<IMessage> messages);
}