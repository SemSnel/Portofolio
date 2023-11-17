using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Factories;

/// <summary>
/// A factory for creating domain messages from domain events.
/// </summary>
public interface IDomainMessageFactory
{
    /// <summary>
    /// Creates a domain message from a domain Event.
    /// </summary>
    /// <param name="domainDomainEvent"> The <see cref="IDomainEvent"/>. </param>
    /// <returns> The <see cref="DomainMessage"/>. </returns>
    public DomainMessage Create(IDomainEvent domainDomainEvent);
}