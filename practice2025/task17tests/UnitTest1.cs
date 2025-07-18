using System;
using System.Threading;
using Xunit;

public class ServerThreadTests
{
    [Fact]
    public void HardStop_StopsImmediately()
    {
        var server = new ServerThread();
        server.Start();

        bool wasExecuted = false;

        server.Enqueue(new TestCommand(() => Thread.Sleep(100)));
        server.Enqueue(new HardStop(server));
        server.Enqueue(new TestCommand(() => wasExecuted = true));

        Thread.Sleep(300);

        Assert.False(wasExecuted);
        Assert.False(server.IsRunning);
    }

    [Fact]
    public void SoftStop_AllowsRemainingCommands()
    {
        var server = new ServerThread();
        server.Start();

        bool command1Executed = false;
        bool command2Executed = false;

        server.Enqueue(new TestCommand(() => command1Executed = true));
        server.Enqueue(new SoftStop(server));
        server.Enqueue(new TestCommand(() => command2Executed = true));

        Thread.Sleep(500);

        Assert.True(command1Executed);
        Assert.True(command2Executed);
        Assert.False(server.IsRunning);
    }
    public class TestCommand : ICommand
    {
        private readonly Action _action;

        public TestCommand(Action action)
        {
            _action = action;
        }

        public void Execute()
        {
            _action();
        }
    }
}