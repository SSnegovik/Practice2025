using System;
using System.Collections.Concurrent;
using System.Threading;

public interface ICommand
{
    void Execute();
}


public class ServerThread
{
    private readonly BlockingCollection<ICommand> _queue = new();
    private Thread? _thread;
    private bool _softStopRequested = false;
    private bool _isRunning = false;

    public int ThreadId => _thread?.ManagedThreadId ?? -1;

    public void Start()
    {
        _isRunning = true;
        _thread = new Thread(() =>
        {
            while (_isRunning)
            {
                try
                {
                    if (!_queue.TryTake(out var command, Timeout.Infinite))
                        continue;

                    command.Execute();

                    if (_softStopRequested && _queue.Count == 0)
                    {
                        _isRunning = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex}");
                }
            }
        });

        _thread.Start();
    }

    public void Enqueue(ICommand command)
    {
        _queue.Add(command);
    }

    public void RequestSoftStop()
    {
        _softStopRequested = true;
    }

    public void RequestHardStop()
    {
        _isRunning = false;
        _queue.CompleteAdding();
    }

    public bool IsRunning => _isRunning;

    public int ManagedThreadId => _thread?.ManagedThreadId ?? -1;
}

public class HardStop : ICommand
{
    private readonly ServerThread _server;

    public HardStop(ServerThread server)
    {
        _server = server;
    }

    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _server.ManagedThreadId)
            throw new InvalidOperationException("Ошибка HardStop");

        _server.RequestHardStop();
    }
}

public class SoftStop : ICommand
{
    private readonly ServerThread _server;

    public SoftStop(ServerThread server)
    {
        _server = server;
    }

    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _server.ManagedThreadId)
            throw new InvalidOperationException("Ошибка SoftStop");

        _server.RequestSoftStop();
    }
}
