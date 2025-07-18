using System.Windows.Input;

public interface ICommand
{
    bool Execute();
}

public interface IScheduler
{
    bool HasCommand();
    ICommand Select();
    void Add(ICommand cmd);
}

public class SimpleScheduler : IScheduler
{
    private readonly Queue<ICommand> newCommands = new();
    private readonly Queue<ICommand> ongoingCommands = new();

    public bool HasCommand() => ongoingCommands.Count > 0 || newCommands.Count > 0;

    public ICommand Select()
    {
        if (ongoingCommands.Count > 0)
            return ongoingCommands.Dequeue();

        if (newCommands.Count > 0)
            return newCommands.Dequeue();

        return null;
    }

    public void Add(ICommand cmd)
    {
        newCommands.Enqueue(cmd);
    }

    public void ReAddOngoing(ICommand cmd)
    {
        ongoingCommands.Enqueue(cmd);
    }
}

public class WorkerThread
{
    private readonly IScheduler scheduler;
    private readonly Thread thread;
    private bool running = true;

    public WorkerThread(IScheduler scheduler)
    {
        this.scheduler = scheduler;
        thread = new Thread(Run);
    }

    public void Start() => thread.Start();

    public void Stop()
    {
        running = false;
        thread.Join();
    }

    private void Run()
    {
        while (running)
        {
            if (scheduler.HasCommand())
            {
                var cmd = scheduler.Select();
                if (cmd != null)
                {
                    bool done = cmd.Execute();
                    if (!done && scheduler is SimpleScheduler simple)
                        simple.ReAddOngoing(cmd);
                }
            }
            else
            {
                Thread.Sleep(10);
            }
        }
    }
}

