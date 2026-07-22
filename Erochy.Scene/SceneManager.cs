namespace Erochy.Scene;

/// <summary>
/// Sistema básico que gerencia a transição de cenas e a retenção da cena ativa atual.
/// </summary>
public class SceneManager : ISceneManager
{
    public Scene? CurrentScene { get; private set; }

    public void LoadScene(Scene scene)
    {
        UnloadCurrentScene();
        CurrentScene = scene;
        CurrentScene.Initialize();
    }

    public void UnloadCurrentScene()
    {
        if (CurrentScene != null)
        {
            CurrentScene.Unload();
            CurrentScene = null;
        }
    }
}
