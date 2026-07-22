using Erochy.ECS;
using System.Collections.Generic;
using System.Linq;

namespace Erochy.Scene;

/// <summary>
/// Representa um mundo ou nível de jogo.
/// Contém o gerenciador de entidades e a árvore raiz dos GameObjects.
/// </summary>
public class Scene
{
    public string Name { get; set; }
    
    /// <summary>
    /// Gerenciador de ciclo de vida das entidades pertencentes a esta cena.
    /// </summary>
    public EntityManager EntityManager { get; } = new();

    public Scene(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Inicia todas as entidades da cena.
    /// </summary>
    public virtual void Initialize()
    {
        // Aqui percorremos todos os componentes para chamar Start()
        foreach (var entity in EntityManager.GetAllEntities())
        {
            var components = entity.GetAllComponents();
            foreach (var comp in components)
            {
                comp.Start();
            }
        }
    }

    /// <summary>
    /// Destrói a cena e todas as entidades contidas.
    /// </summary>
    public virtual void Unload()
    {
        var entities = EntityManager.GetAllEntities().ToList();
        foreach (var entity in entities)
        {
            EntityManager.DestroyEntity(entity.Id);
        }
    }
}
