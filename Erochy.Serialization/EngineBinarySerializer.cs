using System;
using System.Text.Json;

namespace Erochy.Serialization;

/// <summary>
/// Implementação mockada do ISerializer para formato Binário (em Base64).
/// Em uma implementação real, o ideal é alterar a interface para suportar byte[]
/// e utilizar MemoryPack ou MessagePack.
/// </summary>
public class EngineBinarySerializer : ISerializer
{
    public string Serialize<T>(T obj)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
        return Convert.ToBase64String(bytes);
    }

    public T? Deserialize<T>(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return default;
        
        var bytes = Convert.FromBase64String(data);
        return JsonSerializer.Deserialize<T>(bytes);
    }
}
