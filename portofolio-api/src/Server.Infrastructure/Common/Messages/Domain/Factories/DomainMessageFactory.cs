using System.Text.Json;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Mappings;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Factories;

/// <summary>
/// A factory for creating domain messages.
/// </summary>
/// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
/// <param name="dictionary"> The <see cref="DomainMessageTypeNameMapping"/>. </param>
public class DomainMessageFactory(
        IDateTimeProvider dateTimeProvider,
        DomainMessageTypeNameMapping dictionary)
    : IDomainMessageFactory
{
    /// <summary>
    /// Creates a domain message.
    /// </summary>
    /// <param name="domainDomainEvent"> The <see cref="IDomainEvent"/>. </param>
    /// <returns> The <see cref="DomainMessage"/>. </returns>
    /// <exception cref="ArgumentException"> Thrown when the message type is not found. </exception>
    public DomainMessage Create(IDomainEvent domainDomainEvent)
    {
        var hasMessageTypeName = dictionary.TryGetIdentifier(domainDomainEvent.GetType(), out var messageType);

        if (!hasMessageTypeName)
        {
            throw new ArgumentException("Message type not found.");
        }

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        var messageContent = JsonSerializer.Serialize<object>(domainDomainEvent, jsonSerializerOptions);

        if (messageContent is null)
        {
            throw new ArgumentException("Message could not be serialized.");
        }

        var message = DomainMessage.Create(
            messageType,
            messageContent,
            dateTimeProvider.Now());

        return message.Value;
    }
}