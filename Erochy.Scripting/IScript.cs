namespace Erochy.Scripting;

/// <summary>
/// Representa um script compilado em runtime pela engine.
/// Pode ser anexado a um GameObject como um Component, se implementarmos integração avançada.
/// </summary>
public interface IScript
{
    void Execute();
}
