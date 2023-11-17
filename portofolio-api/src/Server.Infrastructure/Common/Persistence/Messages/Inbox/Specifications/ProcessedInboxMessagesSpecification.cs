using Ardalis.Specification;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Specifications;

/// <summary>
/// Gets all processed inbox messages.
/// </summary>
public sealed class ProcessedInboxMessagesSpecification : Specification<InboxMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessedInboxMessagesSpecification"/> class.
    /// </summary>
    public ProcessedInboxMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn != null);
    }
}