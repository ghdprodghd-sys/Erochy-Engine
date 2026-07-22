namespace Erochy.ECS;

/// <summary>
/// Interface base para todos os componentes.
/// Componentes são anexados a GameObjects e contêm dados ou lógicas específicas.
/// </summary>
public interface IComponent
{
    /// <summary>
    /// A entidade à qual este componente está anexado.
    /// </summary>
    GameObject GameObject { get; }

    /// <summary>
    /// Indica se o componente está habilitado.
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// Chamado quando o componente é adicionado ao GameObject.
    /// </summary>
    void Awake();

    /// <summary>
    /// Chamado no primeiro frame em que o componente está ativo.
    /// </summary>
    void Start();

    /// <summary>
    /// Chamado quando o componente é destruído ou removido.
    /// </summary>
    void OnDestroy();
}
