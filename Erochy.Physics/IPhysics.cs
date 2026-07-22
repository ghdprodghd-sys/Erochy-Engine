using System.Numerics;

namespace Erochy.Physics;

/// <summary>
/// Interface para o subsistema de física, lidando com detecção de colisões e simulação de corpos rígidos.
/// </summary>
public interface IPhysics
{
    /// <summary>
    /// A gravidade global aplicada na simulação.
    /// </summary>
    Vector3 Gravity { get; set; }

    /// <summary>
    /// Executa um passo de simulação da física (Raycasts, resoluções de colisões, forças).
    /// </summary>
    /// <param name="fixedDeltaTime">Tempo delta fixo.</param>
    void Simulate(float fixedDeltaTime);

    // Em implementações reais teríamos métodos como Raycast, BoxCast, SphereCast, etc.
}
