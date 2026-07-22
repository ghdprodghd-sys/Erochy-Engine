using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Erochy.Core.Events;

/// <summary>
/// Interface para o sistema de Eventos (Publish/Subscribe).
/// </summary>
public interface IEventBus
{
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
