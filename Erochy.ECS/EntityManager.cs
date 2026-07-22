using System.Collections.Generic;

namespace Erochy.ECS;

/// <summary>
/// Gerencia o ciclo de vida e a memória das entidades (GameObjects) na cena.
/// </summary>
public class EntityManager
{
    private ulong _nextEntityId = 1;
    private readonly Dictionary<ulong, GameObject> _entities = new();
    
    // Podemos integrar o ObjectPool futuramente para os GameObjects em si,
    // caso tenhamos alta rotatividade de criação/destruição.

    public GameObject CreateEntity(string name = "New GameObject")
    {
        var entity = new GameObject(_nextEntityId++, name);
        _entities.Add(entity.Id, entity);
        return entity;
    }

    public void DestroyEntity(ulong id)
    {
        if (_entities.TryGetValue(id, out var entity))
        {
            // Propaga a destruição para os filhos
            var children = new List<GameObject>(entity.Children);
            foreach (var child in children)
            {
                DestroyEntity(child.Id);
            }

            // Avisa componentes da destruição
            var components = entity.GetAllComponents();
            foreach (var comp in components)
            {
                comp.OnDestroy();
            }

            // Remove referências
            if (entity.Parent != null)
            {
                entity.Parent.RemoveChild(entity);
            }

            _entities.Remove(id);
        }
    }

    public GameObject? GetEntity(ulong id)
    {
        return _entities.TryGetValue(id, out var entity) ? entity : null;
    }

    public IEnumerable<GameObject> GetAllEntities() => _entities.Values;
}
