namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities.ValueObjects;

public sealed record InboxMessageType(string Value)
{
    public static implicit operator string(InboxMessageType inboxMessageType) => inboxMessageType.Value;
    public static implicit operator InboxMessageType(string value) => new (value);
}