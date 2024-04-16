using CLI.ConsoleInterface;
using CLI.Events;
using CLI.InputHandlers;

using Core.Abstractions.Configuration;
using Core.Abstractions.System;

namespace CLI.Startup;

public class AppLifetimeImpl(IConfigurationIO stateIo, IFileIO fileIO, IInputParser parser, IConsoleWritter writter) : IDisposable, IAppLifetime
{
    private readonly IConfigurationIO _stateIo = stateIo;
    private readonly IFileIO _fileIo = fileIO;
    private readonly IInputParser _parser = parser;
    private readonly IConsoleWritter _writter = writter;
    private readonly CancellationTokenSource _tokenSource = new();
    private bool disposedValue;

    public event EventHandler<ConsoleInputEventArgs>? ConsoleInput;

    public async Task LoadPrequisitesAsync()
    {
        var token = _tokenSource.Token;
        try
        {
            if (!_stateIo.VerifyWslConfigExistence())
            {
                await _fileIo.CreateEmptyWslConfigFileAsync(token);
                await _writter.WriteStringAsync(@"Could not find .wslconfig file. A empty file was created.");
            }
            await _stateIo.LoadConfigurationFromFileAsync(token);
        }
        catch(IOException ex)
        {
            await _writter.WriteExceptionAsync(ex, token);
        }
        catch(InvalidDataException ex)
        {
            await _writter.WriteExceptionAsync(ex, token);
        }
    }

    public async Task StartLoopAsync()
    {
        var token = _tokenSource.Token;
        string? inputString;

        while (true)
        {
            await Console.Out.WriteAsync(@"> ");
            inputString = await Console.In.ReadLineAsync(token);

            if (IsValidString(inputString))
            {
                try
                {
                    await _parser.ParseInputAsync(inputString, token);
                }
                catch (Exception ex)
                {
                    await _writter.WriteExceptionAsync(ex, token);
                }
                continue;
            }

            await _writter.WriteStringAsync(@"", token);
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
