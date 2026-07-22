namespace Erochy.ECS;

/// <summary>
/// Classe base abstrata para todos os componentes.
/// </summary>
public abstract class Component : IComponent
{
    public GameObject GameObject { get; internal set; } = null!;
    
    public bool IsEnabled { get; set; } = true;

    public virtual void Awake() { }
    
    public virtual void Start() { }
    
    public virtual void OnDestroy() { }
}
