using Ardalis.Specification;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Specifications;

/// <summary>
/// Gets all unprocessed inbox messages.
/// </summary>
public sealed class UnprocessedInboxMessagesSpecification : Specification<InboxMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnprocessedInboxMessagesSpecification"/> class.
    /// </summary>
    public UnprocessedInboxMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn == null);
    }
}