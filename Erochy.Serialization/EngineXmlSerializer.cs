using System.IO;
using System.Xml.Serialization;

namespace Erochy.Serialization;

/// <summary>
/// Implementação do ISerializer utilizando System.Xml.Serialization.
/// </summary>
public class EngineXmlSerializer : ISerializer
{
    public string Serialize<T>(T obj)
    {
        if (obj == null) return string.Empty;

        var serializer = new XmlSerializer(typeof(T));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, obj);
        return stringWriter.ToString();
    }

    public T? Deserialize<T>(string data)
    {
        if (string.IsNullOrWhiteSpace(data)) return default;

        var serializer = new XmlSerializer(typeof(T));
        using var stringReader = new StringReader(data);
        return (T?)serializer.Deserialize(stringReader);
    }
}
