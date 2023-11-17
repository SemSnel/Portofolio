using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Publishers;

/// <summary>
/// A publisher for domain messages.
/// </summary>
/// <param name="context"> The <see cref="IMessagesDatabaseContext"/>. </param>
public sealed class DomainMessagePublisher(IMessagesDatabaseContext context) : IDomainMessagePublisher
{
    /// <inheritdoc/>
    public Task<ErrorOr<Success>> Publish(DomainMessage message)
    {
        context.DomainMessages.Add(message);

        return Task.FromResult<ErrorOr<Success>>(Result.Success);
    }

    /// <inheritdoc/>
    public Task<ErrorOr<Success>> Publish(IEnumerable<DomainMessage> messages)
    {
        context.DomainMessages.AddRange(messages);

        return Task.FromResult<ErrorOr<Success>>(Result.Success);
    }
}