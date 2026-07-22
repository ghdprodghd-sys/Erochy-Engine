namespace Erochy.Core;

/// <summary>
/// Define a interface base para qualquer sistema da engine (ex: RenderSystem, PhysicsSystem).
/// Sistemas são processados pelo Game Loop.
/// </summary>
public interface ISystem
{
    /// <summary>
    /// Chamado uma vez durante a inicialização do sistema.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Chamado a uma taxa fixa (ideal para física).
    /// </summary>
    /// <param name="fixedDeltaTime">Tempo decorrido desde o último FixedUpdate.</param>
    void FixedUpdate(float fixedDeltaTime);

    /// <summary>
    /// Chamado a cada frame.
    /// </summary>
    /// <param name="deltaTime">Tempo decorrido desde o último frame.</param>
    void Update(float deltaTime);

    /// <summary>
    /// Chamado após todos os Updates (ideal para câmeras ou lógicas que dependem de transformações resolvidas).
    /// </summary>
    /// <param name="deltaTime">Tempo decorrido desde o último frame.</param>
    void LateUpdate(float deltaTime);

    /// <summary>
    /// Chamado para renderizar o estado atual.
    /// </summary>
    void Render();

    /// <summary>
    /// Chamado durante o encerramento do sistema para limpar recursos.
    /// </summary>
    void Shutdown();
}
