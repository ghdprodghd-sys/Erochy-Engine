using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Erochy.Core.Time;

namespace Erochy.Core;

/// <summary>
/// A implementação central do ciclo de vida da engine.
/// Orquestra a execução ordenada de todos os sistemas registrados.
/// </summary>
public sealed class Engine : IEngine
{
    private readonly IEnumerable<ISystem> _systems;
    private readonly IEngineLogger _logger;
    private bool _isRunning;

    public Engine(IEnumerable<ISystem> systems, IEngineLogger logger)
    {
        _systems = systems ?? Enumerable.Empty<ISystem>();
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public bool IsRunning => _isRunning;

    public void Initialize()
    {
        _logger.LogInformation("Inicializando Erochy Engine...");

        foreach (var system in _systems)
        {
            _logger.LogDebug($"Inicializando sistema: {system.GetType().Name}");
            system.Initialize();
        }
        
        _logger.LogInformation("Inicialização concluída.");
    }

    public void Run()
    {
        _isRunning = true;
        
        var stopwatch = Stopwatch.StartNew();
        long previousTime = stopwatch.ElapsedTicks;
        double accumulator = 0.0;
        
        _logger.LogInformation("Iniciando Game Loop...");

        while (_isRunning)
        {
            long currentTime = stopwatch.ElapsedTicks;
            float dt = (float)(currentTime - previousTime) / Stopwatch.Frequency;
            previousTime = currentTime;

            // Previne a espiral da morte se houver travamentos ou debug
            if (dt > 0.25f)
                dt = 0.25f;

            GameTime.DeltaTime = dt;
            GameTime.TotalTime += dt;
            accumulator += dt;

            // Fixed Update Loop (Física)
            while (accumulator >= GameTime.FixedDeltaTime)
            {
                foreach (var system in _systems)
                {
                    system.FixedUpdate(GameTime.FixedDeltaTime);
                }
                accumulator -= GameTime.FixedDeltaTime;
            }

            // Update Loop
            foreach (var system in _systems)
            {
                system.Update(GameTime.DeltaTime);
            }

            // Late Update Loop
            foreach (var system in _systems)
            {
                system.LateUpdate(GameTime.DeltaTime);
            }

            // Render Loop
            foreach (var system in _systems)
            {
                system.Render();
            }

            // Um pequeno yield para não usar 100% da CPU caso rodem muito rapido
            // Em uma implementação real com VSync, isso seria gerenciado pela API gráfica.
            Thread.Yield();
        }

        DoShutdown();
    }

    public void Shutdown()
    {
        _isRunning = false;
    }

    private void DoShutdown()
    {
        _logger.LogInformation("Desligando sistemas...");
        foreach (var system in _systems.Reverse())
        {
            _logger.LogDebug($"Desligando sistema: {system.GetType().Name}");
            system.Shutdown();
        }
        _logger.LogInformation("Engine encerrada com sucesso.");
    }
}
