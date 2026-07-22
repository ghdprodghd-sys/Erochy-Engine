using System.Numerics;

namespace Erochy.Input;

/// <summary>
/// Interface unificada para leitura de entradas do usuário (Teclado, Mouse e Gamepad).
/// </summary>
public interface IInput
{
    // OBS: Em uma implementação real, usaríamos Enums (Key, MouseButton) para os parâmetros.
    
    /// <summary>
    /// Verifica se uma tecla/botão está sendo segurada neste exato frame.
    /// </summary>
    bool IsKeyPressed(int keyCode);

    /// <summary>
    /// Verifica se uma tecla/botão foi pressionada(o) neste exato frame.
    /// </summary>
    bool IsKeyDown(int keyCode);

    /// <summary>
    /// Verifica se uma tecla/botão foi solta(o) neste exato frame.
    /// </summary>
    bool IsKeyUp(int keyCode);

    /// <summary>
    /// Posição atual do cursor do mouse na tela.
    /// </summary>
    Vector2 MousePosition { get; }
}
