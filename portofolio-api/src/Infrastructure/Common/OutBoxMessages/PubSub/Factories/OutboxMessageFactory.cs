using System.Text.Json;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Factories;

public class OutboxMessageFactory : IOutboxMessageFactory
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMessageTypeDictionary _dictionary;

    public OutboxMessageFactory(IDateTimeProvider dateTimeProvider, IMessageTypeDictionary dictionary)
    {
        _dateTimeProvider = dateTimeProvider;
        _dictionary = dictionary;
    }

    public OutBoxMessage Create(EventBase domainEvent)
    {
        var hasMessageType = _dictionary.TryGetKey(domainEvent.GetType(), out var messageType);
        
        if (!hasMessageType)
        {
            throw new ArgumentException("Message type not found.");
        }
        
        var messageContent = JsonSerializer.Serialize(domainEvent);
        
        if (messageContent is null)
        {
            throw new ArgumentException("Message could not be serialized.");
        }
        
        return new OutBoxMessage
        {
            Id = Guid.NewGuid(),
            Type = messageType,
            Content = messageContent,
            CreatedOn = _dateTimeProvider.Now()
        };
    }
    
    
}