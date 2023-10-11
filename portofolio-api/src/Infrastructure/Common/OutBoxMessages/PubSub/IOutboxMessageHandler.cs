using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

/// <summary>
/// Handler for outbox messages.
/// Receives a message from the outbox consumer and handles it.
/// </summary>
public interface IOutboxMessageHandler
{
    /// <summary>
    /// Handle the message.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>
    /// A <see cref="ErrorOr{TSuccess}"/> with a <see cref="Success"/> if the message was handled successfully.
    /// </returns>
    public Task<ErrorOr<Success>> Handle(OutBoxMessage message);
}