namespace Erochy.Core;

/// <summary>
/// Representa a interface principal da engine.
/// Orquestra a execução do Game Loop e o gerenciamento do estado global.
/// </summary>
public interface IEngine
{
    /// <summary>
    /// Verifica se a engine está em execução.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Inicializa a engine e todos os sistemas subjacentes.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Inicia o Game Loop principal.
    /// </summary>
    void Run();

    /// <summary>
    /// Solicita o encerramento seguro da engine.
    /// </summary>
    void Shutdown();
}
