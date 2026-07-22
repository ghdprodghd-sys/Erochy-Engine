namespace Erochy.Core;

/// <summary>
/// Define um plugin que pode ser carregado dinamicamente pela engine via Reflection.
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// Nome do plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Versão do plugin.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Inicializa e registra as dependências do plugin na engine.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Limpa os recursos do plugin ao ser descarregado.
    /// </summary>
    void Shutdown();
}
