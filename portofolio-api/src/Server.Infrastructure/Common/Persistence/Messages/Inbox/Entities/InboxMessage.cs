using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;

/// <summary>
/// A Event message that is stored in the inbox.
/// </summary>
public sealed class InboxMessage : BaseAggregateRoot<InboxMessageId>
{
    /// <summary>
    /// Gets or sets the type of the inbox message.
    /// </summary>
    public InboxMessageType Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the data of the inbox message.
    /// </summary>
    public InboxMessageData Data { get; set; } = default!;

    /// <summary>
    /// Gets the date and time when the inbox message was created.
    /// </summary>
    public System.DateTime? ProcessedOn { get; private set; }

    /// <summary>
    /// Creates a new inbox message.
    /// </summary>
    /// <param name="type"> The type of the inbox message. </param>
    /// <param name="data"> The data of the inbox message. </param>
    /// <returns> The <see cref="ErrorOr{InboxMessage}"/>. </returns>
    public static ErrorOr<Created<InboxMessageId>> Create(
        InboxMessageType type,
        InboxMessageData data)
    {
        var result = new InboxMessage
        {
            Id = new InboxMessageId(Guid.NewGuid()),
            Type = type,
            Data = data,
        };

        return new Created<InboxMessageId>(result.Id);
    }

    /// <summary>
    /// Processes the inbox message.
    /// </summary>
    /// <param name="processedAt"> The date and time when the inbox message was processed. </param>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    public ErrorOr<Success> Process(System.DateTime processedAt)
    {
        if (ProcessedOn.HasValue)
        {
            return Error.Validation("InboxMessage already processed");
        }

        ProcessedOn = processedAt;

        return Result.Success;
    }
}