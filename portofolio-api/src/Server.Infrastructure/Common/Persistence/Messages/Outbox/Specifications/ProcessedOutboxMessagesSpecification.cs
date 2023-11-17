using Ardalis.Specification;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.Specifications;

/// <summary>
/// Gets all processed outbox messages.
/// </summary>
public sealed class ProcessedOutboxMessagesSpecification : Specification<OutboxMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessedOutboxMessagesSpecification"/> class.
    /// </summary>
    public ProcessedOutboxMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn != null);
    }
}