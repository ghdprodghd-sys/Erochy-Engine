namespace Erochy.ECS;

/// <summary>
/// Interface base para qualquer entidade do jogo.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Identificador único da entidade.
    /// </summary>
    ulong Id { get; }

    /// <summary>
    /// Nome descritivo da entidade.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Indica se a entidade está ativa no mundo.
    /// </summary>
    bool IsActive { get; set; }
}
