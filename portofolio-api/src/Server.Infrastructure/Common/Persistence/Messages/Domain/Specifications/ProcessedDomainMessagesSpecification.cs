using Ardalis.Specification;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Specifications;

/// <summary>
/// A specification for selecting processed domain messages.
/// </summary>
public sealed class ProcessedDomainMessagesSpecification : Specification<DomainMessage>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessedDomainMessagesSpecification"/> class.
    /// </summary>
    public ProcessedDomainMessagesSpecification()
    {
        Query.Where(message => message.ProcessedOn != null);
    }
}