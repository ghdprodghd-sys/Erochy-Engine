using System;
using Erochy.Core;
using Erochy.Core.Events;
using Erochy.Core.Time;
using Erochy.ECS;
using Erochy.Scene;
using Erochy.Assets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Erochy.Runtime;

/// <summary>
/// O construtor principal da engine. Encapsula o HostBuilder do .NET para gerenciar
/// injeção de dependência e inicialização da aplicação num estilo Clean Architecture.
/// </summary>
public class ErochyEngineBuilder
{
    private readonly HostApplicationBuilder _builder;

    public ErochyEngineBuilder(string[]? args = null)
    {
        _builder = Host.CreateApplicationBuilder(args);
        
        // Registro dos sistemas base da Engine
        _builder.Services.AddSingleton<IEngineLogger, EngineLoggerWrapper>();
        _builder.Services.AddSingleton<IEventBus, EventBus>();
        _builder.Services.AddSingleton<ISceneManager, SceneManager>();
        _builder.Services.AddSingleton<IAssetManager>(sp => new AssetManager("Assets"));
        _builder.Services.AddSingleton<IEngine, Engine>();
    }

    public IServiceCollection Services => _builder.Services;
    public ILoggingBuilder Logging => _builder.Logging;

    public void AddSystem<TSystem>() where TSystem : class, ISystem
    {
        _builder.Services.AddSingleton<ISystem, TSystem>();
    }

    private IHost? _host;

    public IServiceProvider? Provider => _host?.Services;

    public IEngine Build()
    {
        _host = _builder.Build();
        var engine = _host.Services.GetRequiredService<IEngine>();
        return engine;
    }
}

/// <summary>
/// Wrapper para adaptar o ILogger do .NET para a interface interna da engine
/// sem acoplar fortemente o núcleo da engine ao Microsoft.Extensions.Logging.
/// </summary>
internal class EngineLoggerWrapper : IEngineLogger
{
    private readonly ILogger<EngineLoggerWrapper> _logger;

    public EngineLoggerWrapper(ILogger<EngineLoggerWrapper> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message) => _logger.LogInformation(message);
    public void LogWarning(string message) => _logger.LogWarning(message);
    public void LogError(string message, Exception? exception = null) => _logger.LogError(exception, message);
    public void LogDebug(string message) => _logger.LogDebug(message);
}
