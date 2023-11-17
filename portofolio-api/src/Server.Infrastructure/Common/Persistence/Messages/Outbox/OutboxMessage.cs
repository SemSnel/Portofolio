using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox;

/// <summary>
/// An <see cref="OutboxMessage"/> is a message that is sent to a message broker.
/// </summary>
public sealed class OutboxMessage : BaseAggregateRoot<OutboxMessageId>
{
    /// <summary>
    /// Gets the data of the message.
    /// </summary>
    public OutboxMessageData Data { get; private set; }

    /// <summary>
    /// Gets the type of the message.
    /// </summary>
    public OutboxMessageType Type { get; private set; }

    /// <summary>
    /// Gets the date and time the message was processed.
    /// </summary>
    public System.DateTime? ProcessedOn { get; private set; }

    /// <summary>
    /// Creates a new <see cref="OutboxMessage"/>.
    /// </summary>
    /// <param name="data"> The data of the message. </param>
    /// <param name="type"> The type of the message. </param>
    /// <returns> The <see cref="OutboxMessage"/>. </returns>
    public static ErrorOr<OutboxMessage> Create(OutboxMessageData data, OutboxMessageType type)
    {
        var id = new OutboxMessageId(Guid.NewGuid());

        var message = new OutboxMessage
        {
            Id = id,
            Data = data,
            Type = type,
            ProcessedOn = null,
        };

        return message;
    }

    /// <summary>
    /// Marks the message as processed.
    /// </summary>
    /// <param name="processedAt"> The date and time the message was processed. </param>
    /// <returns> The <see cref="Result"/>. </returns>
    public ErrorOr<Success> MarkAsProcessed(System.DateTime processedAt)
    {
        if (ProcessedOn.HasValue)
        {
            return Error.Validation($"Message '{Id}' is already processed.");
        }

        ProcessedOn = processedAt;

        return default(Success);
    }
}