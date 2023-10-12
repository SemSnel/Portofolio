using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.OutBoxMessages.PubSub;

/// <summary>
/// Interface for outbox subscribers.
/// This interface is used to mark a class as an outbox subscriber.
/// </summary>
public interface IOutBoxMessageConsumer
{
    public Task<ErrorOr<Success>> Consume(CancellationToken cancellationToken = default);
}