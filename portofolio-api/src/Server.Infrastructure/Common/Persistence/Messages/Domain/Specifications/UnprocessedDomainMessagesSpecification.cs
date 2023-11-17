using Ardalis.Specification;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Specifications;

/// <summary>
/// Gets unprocessed domain messages.
/// </summary>
public sealed class UnprocessedDomainMessagesSpecification : Specification<DomainMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnprocessedDomainMessagesSpecification"/> class.
    /// </summary>
    public UnprocessedDomainMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn == null);
    }
}