using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Erochy.Core.Events;

/// <summary>
/// Implementação Thread-Safe do IEventBus, sem uso de Reflection durante a publicação.
/// Utiliza um cache de delegates fortemente tipados internamente.
/// </summary>
public class EventBus : IEventBus
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _handlers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        var handlersList = _handlers.GetOrAdd(eventType, _ => new List<Delegate>());
        
        lock (handlersList)
        {
            if (!handlersList.Contains(handler))
            {
                handlersList.Add(handler);
            }
        }
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (_handlers.TryGetValue(eventType, out var handlersList))
        {
            lock (handlersList)
            {
                handlersList.Remove(handler);
            }
        }
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var eventType = typeof(TEvent);
        if (_handlers.TryGetValue(eventType, out var handlersList))
        {
            List<Delegate> handlersCopy;
            lock (handlersList)
            {
                // Copiamos a lista para evitar exceptions caso alguém faça un/subscribe durante o Publish
                handlersCopy = new List<Delegate>(handlersList);
            }

            foreach (var handler in handlersCopy)
            {
                if (handler is Action<TEvent> typedHandler)
                {
                    typedHandler(@event);
                }
            }
        }
    }
}
