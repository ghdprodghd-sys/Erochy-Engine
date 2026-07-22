using System;

namespace Erochy.Assets;

/// <summary>
/// Gerencia o ciclo de vida de recursos e lida com o cache de memória.
/// </summary>
public interface IAssetManager
{
    /// <summary>
    /// Evento disparado quando um asset é modificado no disco (para Hot Reload).
    /// </summary>
    event Action<string>? OnAssetChanged;

    /// <summary>
    /// Carrega e faz cache de um recurso.
    /// </summary>
    T Load<T>(string path) where T : IResource, new();

    /// <summary>
    /// Descarrega um recurso da memória.
    /// </summary>
    void Unload(string path);

    /// <summary>
    /// Limpa todos os recursos cacheados não mais em uso.
    /// </summary>
    void ClearCache();
}
