namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;

public readonly record struct OutboxMessageType(string Value)
{
    public static implicit operator string(OutboxMessageType data) => data.Value;
    public static implicit operator OutboxMessageType(string value) => new (value);
}