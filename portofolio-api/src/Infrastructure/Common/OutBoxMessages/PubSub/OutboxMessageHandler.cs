using System.Text.Json;
using SemSnel.Portofolio.Domain.Common.Entities;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub.Dictionaries;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.PubSub;

public sealed class OutboxMessageHandler : IOutboxMessageHandler
{
    private readonly IMessageTypeDictionary _dictionary;
    private readonly IPublisher _publisher;

    public OutboxMessageHandler(IMessageTypeDictionary dictionary, IPublisher publisher)
    {
        _dictionary = dictionary;
        _publisher = publisher;
    }

    public async Task<ErrorOr<Success>> Handle(OutBoxMessage message)
    {
        var hasMessageType = _dictionary.TryGetValue(message.Type, out var messageType);
        
        if (!hasMessageType)
        {
            return Error.Failure("Message type not found.");
        }
        
        var messageContent = JsonSerializer.Deserialize(message.Content, messageType);
        
        if (messageContent is null)
        {
            return Error.Failure("Message could not be deserialized.");
        }

        if (messageContent is not EventBase)
        {
            return Error.Failure($"Message is not of type {nameof(EventBase)}.");
        }

        try
        {
            await _publisher.Publish(messageContent);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }
}