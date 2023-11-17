using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Publishers;

/// <summary>
/// Publisher for domain messages.
/// </summary>
public interface IDomainMessagePublisher
{
    /// <summary>
    /// Publishes a message to the outbox.
    /// </summary>
    /// <param name="message"> The <see cref="DomainMessage"/>. </param>
    /// <returns> The <see cref="Task{TResult}"/>. </returns>
    public Task<ErrorOr<Success>> Publish(DomainMessage message);

    /// <summary>
    /// Publishes a list of messages to the outbox.
    /// </summary>
    /// <param name="messages"> The <see cref="IEnumerable{T}"/> of <see cref="DomainMessage"/>. </param>
    /// <returns> The <see cref="Task{TResult}"/>. </returns>
    public Task<ErrorOr<Success>> Publish(IEnumerable<DomainMessage> messages);
}