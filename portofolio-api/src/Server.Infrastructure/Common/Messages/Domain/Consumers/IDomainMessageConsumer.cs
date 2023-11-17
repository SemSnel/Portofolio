using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Consumers;

/// <summary>
/// Interface for outbox subscribers.
/// This interface is used to mark a class as an outbox subscriber.
/// </summary>
public interface IDomainMessageConsumer
{
    /// <summary>
    /// Consumes messages from the outbox.
    /// </summary>
    /// <param name="cancellationToken"> The <see cref="CancellationToken"/>. </param>
    /// <returns> The <see cref="Task{TResult}"/>. </returns>
    public Task<ErrorOr<Success>> Consume(CancellationToken cancellationToken = default);
}