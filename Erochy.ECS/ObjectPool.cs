using System;
using System.Collections.Concurrent;

namespace Erochy.ECS;

/// <summary>
/// Implementa Object Pooling genérico para reaproveitamento de componentes e outras instâncias frequentes.
/// Evita alocações excessivas e coletas de lixo (GC) indesejadas.
/// </summary>
public class ObjectPool<T> where T : new()
{
    private readonly ConcurrentBag<T> _objects = new();

    public T Get()
    {
        return _objects.TryTake(out T? item) ? item : new T();
    }

    public void Return(T item)
    {
        if (item == null) return;
        _objects.Add(item);
    }
}
