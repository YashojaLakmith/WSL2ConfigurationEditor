using CLI.Events;

namespace CLI.Startup;

public interface IAppLifetime : IConsoleLoopEvents
{
    Task StartLoopAsync();
}