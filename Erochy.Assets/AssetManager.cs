using System;
using System.Collections.Concurrent;
using System.IO;

namespace Erochy.Assets;

/// <summary>
/// Implementação do gerenciador de recursos utilizando cache em memória e File Watcher.
/// </summary>
public class AssetManager : IAssetManager, IDisposable
{
    private readonly ConcurrentDictionary<string, IResource> _cache = new();
    private readonly FileSystemWatcher? _fileWatcher;
    private readonly string _assetsRootPath;

    public event Action<string>? OnAssetChanged;

    public AssetManager(string assetsRootPath)
    {
        _assetsRootPath = assetsRootPath;

        if (Directory.Exists(_assetsRootPath))
        {
            _fileWatcher = new FileSystemWatcher(_assetsRootPath)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            _fileWatcher.Changed += OnFileChanged;
            _fileWatcher.Renamed += OnFileChanged;
        }
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        // Dispara o evento de Hot Reload (ex: scripts, shaders, json modificado)
        OnAssetChanged?.Invoke(e.FullPath);
    }

    public T Load<T>(string path) where T : IResource, new()
    {
        var fullPath = Path.Combine(_assetsRootPath, path);
        
        // Verifica o cache primeiro
        if (_cache.TryGetValue(fullPath, out var cachedResource))
        {
            if (cachedResource is T resource)
            {
                return resource;
            }
            throw new InvalidOperationException($"Recurso cacheados não é do tipo {typeof(T).Name}");
        }

        // Simula o carregamento ou instancia um novo IResource customizado vazio
        var newResource = new T();
        
        // Implementação real seria um Load via factory baseada na extensão, 
        // mas setamos direto no dicionário por hora.
        _cache[fullPath] = newResource;

        return newResource;
    }

    public void Unload(string path)
    {
        var fullPath = Path.Combine(_assetsRootPath, path);
        if (_cache.TryRemove(fullPath, out var resource))
        {
            resource.Dispose();
        }
    }

    public void ClearCache()
    {
        foreach (var resource in _cache.Values)
        {
            resource.Dispose();
        }
        _cache.Clear();
    }

    public void Dispose()
    {
        ClearCache();
        _fileWatcher?.Dispose();
    }
}
