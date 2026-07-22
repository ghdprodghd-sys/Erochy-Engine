using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Erochy.Core.Tasks;

/// <summary>
/// Scheduler simples para execução de tarefas assíncronas e em background na engine.
/// Evita gargalos na thread principal do Game Loop.
/// </summary>
public class Scheduler
{
    private readonly ConcurrentQueue<Action> _mainThreadQueue = new();

    /// <summary>
    /// Enfileira uma ação para ser executada em background (Thread Pool).
    /// </summary>
    public void RunInBackground(Action action)
    {
        Task.Run(() => 
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // Em um cenário real logaríamos no IEngineLogger
                Console.WriteLine($"Erro na tarefa de background: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Enfileira uma ação para ser executada na Main Thread durante o próximo frame.
    /// Útil quando o background termina um cálculo e precisa atualizar a UI/Scene.
    /// </summary>
    public void RunOnMainThread(Action action)
    {
        _mainThreadQueue.Enqueue(action);
    }

    /// <summary>
    /// Chamado pela engine (ex: final do Update) para processar as tarefas pendentes da Main Thread.
    /// </summary>
    public void ProcessMainThreadTasks()
    {
        while (_mainThreadQueue.TryDequeue(out var action))
        {
            action();
        }
    }
}
