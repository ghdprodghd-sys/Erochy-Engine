using System.Collections.Generic;

namespace Erochy.Scene;

/// <summary>
/// Interface para o sistema que gerencia as cenas do jogo (estados do mundo).
/// </summary>
public interface ISceneManager
{
    /// <summary>
    /// A cena que está atualmente ativa.
    /// </summary>
    Scene? CurrentScene { get; }

    /// <summary>
    /// Carrega uma nova cena e descarrega a anterior.
    /// </summary>
    void LoadScene(Scene scene);

    /// <summary>
    /// Descarrega a cena atual limpando as entidades.
    /// </summary>
    void UnloadCurrentScene();
}
