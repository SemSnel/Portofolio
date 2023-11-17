namespace SemSnel.Portofolio.Domain._Common.Entities.Events.Outbox;

/// <summary>
/// An outbox event is an event that is sent to a external source.
/// </summary>
public interface IOutboxEvent : INotification
{
}