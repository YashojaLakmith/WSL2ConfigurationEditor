using CLI.ConsoleInterface;

using Core.Abstractions.Configuration;
using Core.Abstractions.System;

namespace CLI.Startup;

public class StartupProviderImpl : IStartupProvider
{
    private readonly IFileIO _fileIo;
    private readonly IConfigurationIO _stateIo;
    private readonly IConsoleWritter _writter;

    public StartupProviderImpl(IFileIO fileIo, IConfigurationIO stateIo, IConsoleWritter writter)
    {
        _fileIo = fileIo;
        _stateIo = stateIo;
        _writter = writter;
    }

    public async Task ConfigureStartupAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            if (!_stateIo.VerifyWslConfigExistence())
            {
                await _fileIo.CreateEmptyWslConfigFileAsync(cancellationToken);
                await _writter.WriteStringAsync(@"Could not find .wslconfig file. A empty file was created.", cancellationToken);
            }
            await _stateIo.LoadConfigurationFromFileAsync(cancellationToken);
        }
        catch (IOException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
        catch (InvalidDataException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }
}
