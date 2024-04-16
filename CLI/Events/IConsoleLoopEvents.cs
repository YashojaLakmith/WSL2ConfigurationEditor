namespace CLI.Events;

public interface IConsoleLoopEvents
{
    event EventHandler<ConsoleInputEventArgs>? ConsoleInput;
}