namespace Erochy.Core;

/// <summary>
/// Interface para logging agnóstico, pode ser implementada encapsulando o Microsoft.Extensions.Logging.
/// </summary>
public interface IEngineLogger
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? exception = null);
    void LogDebug(string message);
}
