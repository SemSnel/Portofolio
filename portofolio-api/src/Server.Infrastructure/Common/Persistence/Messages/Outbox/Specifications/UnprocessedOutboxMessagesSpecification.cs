using Ardalis.Specification;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.Specifications;

/// <summary>
/// Gets all unprocessed outbox messages.
/// </summary>
public sealed class UnprocessedOutboxMessagesSpecification : Specification<OutboxMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnprocessedOutboxMessagesSpecification"/> class.
    /// </summary>
    public UnprocessedOutboxMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn == null);
    }
}