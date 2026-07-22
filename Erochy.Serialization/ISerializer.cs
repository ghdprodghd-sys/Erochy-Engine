namespace Erochy.Serialization;

/// <summary>
/// Interface para os sistemas de serialização da engine.
/// Útil para salvar e carregar Cenas, configurações de projeto, etc.
/// </summary>
public interface ISerializer
{
    /// <summary>
    /// Converte um objeto em uma string no formato suportado.
    /// </summary>
    string Serialize<T>(T obj);

    /// <summary>
    /// Restaura um objeto a partir de uma string.
    /// </summary>
    T? Deserialize<T>(string data);
}
