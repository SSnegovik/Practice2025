using Xunit;
using System.Collections.Generic;

public class SchedulerTests
{
    [Fact]
    public void AddCommand()
    {
        var scheduler = new SimpleScheduler();
        var cmd = new TestCommand(1);

        scheduler.Add(cmd);

        Assert.True(scheduler.HasCommand());
    }

    [Fact]
    public void SelectCommand()
    {
        var scheduler = new SimpleScheduler();
        var cmd = new TestCommand(1);

        scheduler.Add(cmd);
        var selected = scheduler.Select();

        Assert.Same(cmd, selected);
    }

    [Fact]
    public void IncompleteCommand()
    {
        var scheduler = new SimpleScheduler();
        var cmd = new TestCommand(3);

        scheduler.Add(cmd);

        var first = scheduler.Select();
        Assert.False(first.Execute());

        scheduler.ReAddOngoing(first);
        var second = scheduler.Select();

        Assert.Same(first, second);
    }

    [Fact]
    public void WorkerThread()
    {
        var scheduler = new SimpleScheduler();
        var cmd = new TestCommand(5);

        scheduler.Add(cmd);

        var worker = new WorkerThread(scheduler);
        worker.Start();

        Thread.Sleep(1000);

        worker.Stop();

        Assert.True(cmd.IsDone);
    }
}

public class TestCommand : ICommand
{
    private int stepsLeft;
    public bool IsDone => stepsLeft <= 0;

    public TestCommand(int steps)
    {
        stepsLeft = steps;
    }

    public bool Execute()
    {
        Thread.Sleep(50);
        stepsLeft--;
        return stepsLeft <= 0;
    }
}
