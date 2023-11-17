namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities.ValueObjects;

public sealed record InboxMessageData(string Value)
{
    public static implicit operator string(InboxMessageData inboxMessageData) => inboxMessageData.Value;
    public static implicit operator InboxMessageData(string value) => new (value);
}