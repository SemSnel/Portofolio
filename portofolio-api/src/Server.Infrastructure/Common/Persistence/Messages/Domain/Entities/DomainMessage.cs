using SemSnel.Portofolio.Domain._Common.Entities;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;

/// <summary>
/// A domain message.
/// Domain messages are used to communicate between bounded contexts in the system.
/// </summary>
public sealed class DomainMessage : BaseAggregateRoot<DomainMessageId>
{
    /// <summary>
    /// Gets or sets the type of the domain message.
    /// </summary>
    public DomainMessageType Type { get; set; } = default!;

    /// <summary>
    /// Gets or sets the data of the domain message.
    /// </summary>
    public DomainMessageData Data { get; set; } = default!;

    /// <summary>
    /// Gets the date and time when the domain message was created.
    /// </summary>
    public System.DateTime CreatedOn { get; private set; }

    /// <summary>
    /// Gets the date and time when the domain message was processed.
    /// </summary>
    public System.DateTime? ProcessedOn { get; private set; }

    /// <summary>
    /// Creates a new domain message.
    /// </summary>
    /// <param name="type"> The type of the domain message. </param>
    /// <param name="data"> The data of the domain message. </param>
    /// <param name="createdOn"> The date and time when the domain message was created. </param>
    /// <returns> The <see cref="ErrorOr{DomainMessage}"/>. </returns>
    public static ErrorOr<Created<DomainMessage>> Create(
        DomainMessageType type,
        DomainMessageData data,
        System.DateTime createdOn)
    {
        var result = new DomainMessage
        {
            Id = new DomainMessageId(Guid.NewGuid()),
            Type = type,
            Data = data,
            CreatedOn = createdOn,
        };

        return result.ToCreated();
    }

    /// <summary>
    /// Processes the domain message.
    /// </summary>
    /// <param name="processedAt"> The date and time when the domain message was processed. </param>
    /// <returns> The <see cref="ErrorOr{Success}"/>. </returns>
    public ErrorOr<Success> Process(System.DateTime processedAt)
    {
        if (ProcessedOn.HasValue)
        {
            return Error.Validation("DomainMessage already processed");
        }

        ProcessedOn = processedAt;

        return Result.Success;
    }
}