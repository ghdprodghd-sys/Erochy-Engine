namespace Erochy.Core.Time;

/// <summary>
/// Mantém as informações de tempo globais da engine.
/// </summary>
public static class GameTime
{
    /// <summary>
    /// O tempo em segundos decorrido desde o último frame (Update).
    /// </summary>
    public static float DeltaTime { get; internal set; }

    /// <summary>
    /// O tempo em segundos de cada passo de física (FixedUpdate).
    /// </summary>
    public static float FixedDeltaTime { get; internal set; } = 0.02f; // 50 Hz por padrão

    /// <summary>
    /// O tempo total decorrido em segundos desde o início da engine.
    /// </summary>
    public static float TotalTime { get; internal set; }
}
