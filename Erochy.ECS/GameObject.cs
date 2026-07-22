using System;
using System.Collections.Generic;
using System.Linq;

namespace Erochy.ECS;

/// <summary>
/// Representa uma entidade na cena. Um GameObject contém múltiplos Componentes.
/// Implementação de ECS Híbrido (Orientação a Objetos no topo, Dados na base).
/// </summary>
public sealed class GameObject : IEntity
{
    private readonly List<IComponent> _components = new();

    public ulong Id { get; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// O pai deste GameObject no Scene Graph.
    /// </summary>
    public GameObject? Parent { get; private set; }

    /// <summary>
    /// Os filhos deste GameObject.
    /// </summary>
    public IReadOnlyList<GameObject> Children => _children;
    private readonly List<GameObject> _children = new();

    internal GameObject(ulong id, string name)
    {
        Id = id;
        Name = name;
    }

    /// <summary>
    /// Adiciona um novo componente do tipo especificado.
    /// </summary>
    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T
        {
            GameObject = this
        };
        
        _components.Add(component);
        component.Awake();
        
        return component;
    }

    /// <summary>
    /// Recupera um componente do tipo especificado.
    /// </summary>
    public T? GetComponent<T>() where T : class, IComponent
    {
        return _components.FirstOrDefault(c => c is T) as T;
    }

    /// <summary>
    /// Recupera todos os componentes do tipo especificado.
    /// </summary>
    public IEnumerable<T> GetComponents<T>() where T : class, IComponent
    {
        return _components.OfType<T>();
    }

    /// <summary>
    /// Remove um componente do GameObject.
    /// </summary>
    public bool RemoveComponent<T>(T component) where T : Component
    {
        if (_components.Remove(component))
        {
            component.OnDestroy();
            component.GameObject = null!;
            return true;
        }
        return false;
    }

    public void AddChild(GameObject child)
    {
        if (child.Parent != null)
        {
            child.Parent.RemoveChild(child);
        }
        child.Parent = this;
        _children.Add(child);
    }

    public void RemoveChild(GameObject child)
    {
        if (_children.Remove(child))
        {
            child.Parent = null;
        }
    }

    /// <summary>
    /// Retorna todos os componentes deste GameObject.
    /// </summary>
    public IReadOnlyList<IComponent> GetAllComponents() => _components;
}
