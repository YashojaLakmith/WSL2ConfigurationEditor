using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;
using CLI.Abstractions.Startup;

namespace CLI.Startup;

public class AppLifetimeImpl : IDisposable, IAppLifetime
{
    private readonly IInputParser _parser;
    private readonly IConsoleWritter _writter;
    private readonly CancellationTokenSource _tokenSource = new();
    private bool disposedValue;

    public AppLifetimeImpl(IInputParser parser, IConsoleWritter writter)
    {
        _parser = parser;
        _writter = writter;
    }

    public async Task StartLoopAsync()
    {
        CancellationToken token = _tokenSource.Token;
        string? inputString;

        while (true)
        {
            await Console.Out.WriteAsync(@"> ");
            inputString = await Console.In.ReadLineAsync(token);

            if (IsValidString(inputString))
            {
                try
                {
                    await _parser.ParseInputAsync(inputString!, token);
                }
                catch (Exception ex)
                {
                    await _writter.WriteExceptionAsync(ex, token);
                }
                continue;
            }

            await _writter.WriteStringAsync(@" ", token);
        }
    }

    private static bool IsValidString(string? input)
    {
        return !string.IsNullOrWhiteSpace(input)
            && !input.Equals(string.Empty);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _tokenSource.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
