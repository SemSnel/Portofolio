namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;

public readonly record struct OutboxMessageData(string Value)
{
    public static implicit operator string(OutboxMessageData data) => data.Value;
    public static implicit operator OutboxMessageData(string value) => new (value);
}