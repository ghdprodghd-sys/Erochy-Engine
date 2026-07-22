using System.Text.Json;

namespace Erochy.Serialization;

/// <summary>
/// Implementação do ISerializer utilizando System.Text.Json.
/// </summary>
public class EngineJsonSerializer : ISerializer
{
    private readonly JsonSerializerOptions _options;

    public EngineJsonSerializer()
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true // Necessário para fields públicos e structs em jogos
        };
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, _options);
    }

    public T? Deserialize<T>(string data)
    {
        return JsonSerializer.Deserialize<T>(data, _options);
    }
}
