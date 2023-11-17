using System.Text.Json;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Mappings;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Server.Application.Common.Messages;

namespace SemSnel.Portofolio.Infrastructure.Common.Messages.Domain.Handlers;

/// <summary>
/// A handler for domain messages.
/// </summary>
/// <param name="dictionary"> The <see cref="DomainMessageTypeNameMapping"/>. </param>
/// <param name="publisher"> The <see cref="IPublisher"/>. </param>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
public sealed class DomainMessageHandler(
        DomainMessageTypeNameMapping dictionary,
        IPublisher publisher,
        ILogger<DomainMessageHandler> logger
        )
    : IDomainMessageHandler
{
    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> Handle(DomainMessage message)
    {
        var hasMessageType = dictionary.TryGetType(message.Type, out var messageType);

        if (!hasMessageType)
        {
            // success because we don't want to retry this message, but log that it was not handled, because it is not a known message type
            logger.LogInformation("Message type {MessageType} not found.", message.Type);

            return Result.Success;
        }

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
        };

        var messageContent = JsonSerializer.Deserialize(message.Data.Value, messageType, serializerOptions);

        if (messageContent is null)
        {
            return Error.Failure("Message could not be deserialized.");
        }

        if (messageContent is not IDomainEvent)
        {
            return Error.Failure($"Message is not of type {nameof(IDomainEvent)}.");
        }

        try
        {
            // make generic type
            var notificationType = typeof(DomainMessageNotification<>).MakeGenericType(messageType);

            // create notification
            var notification = Activator.CreateInstance(notificationType, messageContent);

            if (notification is not null)
            {
                await publisher.Publish(notification);
            }

            return Result.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while publishing message {MessageId}. Message: {Message}", message.Id, messageContent);

            return Error.Failure(e.Message);
        }
    }
}