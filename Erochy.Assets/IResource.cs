using System;

namespace Erochy.Assets;

/// <summary>
/// Interface básica para qualquer recurso/asset carregado na engine (texturas, sons, modelos, scripts).
/// </summary>
public interface IResource : IDisposable
{
    /// <summary>
    /// Caminho ou identificador único do recurso.
    /// </summary>
    string Path { get; }
    
    /// <summary>
    /// Indica se o recurso foi carregado com sucesso na memória.
    /// </summary>
    bool IsLoaded { get; }
}
