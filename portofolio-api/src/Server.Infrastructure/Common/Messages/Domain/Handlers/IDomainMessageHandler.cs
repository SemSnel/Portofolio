using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Handlers;

/// <summary>
/// Handler for outbox messages.
/// Receives a message from the outbox consumer and handles it.
/// </summary>
public interface IDomainMessageHandler
{
    /// <summary>
    /// Handle the message.
    /// </summary>
    /// <param name="message"> The <see cref="DomainMessage"/>. </param>
    /// <returns>
    /// A <see cref="ErrorOr{TSuccess}"/> with a <see cref="Success"/> if the message was handled successfully.
    /// </returns>
    public Task<ErrorOr<Success>> Handle(DomainMessage message);
}