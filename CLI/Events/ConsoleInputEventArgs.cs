namespace CLI.Events;

public class ConsoleInputEventArgs : EventArgs
{
    public string Text { get; }

    public ConsoleInputEventArgs(string text)
    {
        Text = text;
    }
}
