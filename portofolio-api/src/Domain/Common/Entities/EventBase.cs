﻿namespace SemSnel.Portofolio.Domain.Common.Entities;

/// <summary>
/// Base class for domain events.
/// </summary>
public abstract class EventBase : INotification
{
    public Guid EventId { get; } = Guid.NewGuid();
}
