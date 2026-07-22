using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Erochy.Core;

namespace Erochy.Runtime;

/// <summary>
/// Responsável por carregar plugins dinâmicos (DLLs) via Reflection apenas durante a inicialização (Boot).
/// Após inicializado, a engine usará as instâncias através de interfaces puras, sem reflection.
/// </summary>
public class PluginLoader
{
    private readonly List<IPlugin> _loadedPlugins = new();

    /// <summary>
    /// Retorna todos os plugins atualmente carregados e inicializados.
    /// </summary>
    public IReadOnlyList<IPlugin> Plugins => _loadedPlugins;

    /// <summary>
    /// Procura por DLLs em um diretório e carrega tipos que implementam IPlugin.
    /// Deve ser chamado ANTES do Game Loop (Run).
    /// </summary>
    public void LoadPluginsFromDirectory(string path, IEngineLogger logger)
    {
        if (!Directory.Exists(path))
        {
            logger.LogWarning($"Diretório de plugins não encontrado: {path}");
            return;
        }

        var dllFiles = Directory.GetFiles(path, "*.dll");
        foreach (var dll in dllFiles)
        {
            try
            {
                var assembly = Assembly.LoadFrom(dll);
                var pluginTypes = assembly.GetTypes()
                                          .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in pluginTypes)
                {
                    if (Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        logger.LogInformation($"Carregando plugin: {plugin.Name} v{plugin.Version}");
                        plugin.Initialize();
                        _loadedPlugins.Add(plugin);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao carregar DLL de plugin: {dll}", ex);
            }
        }
    }

    /// <summary>
    /// Descarrega todos os plugins de forma segura.
    /// </summary>
    public void UnloadAll(IEngineLogger logger)
    {
        foreach (var plugin in _loadedPlugins)
        {
            logger.LogInformation($"Descarregando plugin: {plugin.Name}");
            plugin.Shutdown();
        }
        _loadedPlugins.Clear();
    }
}
