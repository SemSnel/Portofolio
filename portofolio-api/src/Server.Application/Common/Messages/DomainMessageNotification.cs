using SemSnel.Portofolio.Domain._Common.Entities.Events.Domain;
using SemSnel.Portofolio.Domain._Common.Entities.Events.Inbox;

namespace SemSnel.Portofolio.Server.Application.Common.Messages;

public sealed record DomainMessageNotification<T>(T Event) : INotification
    where T : IDomainEvent
{
}

public sealed record InboundMessageNotification<T>(T Event) : INotification
    where T : IInboxEvent
{
}