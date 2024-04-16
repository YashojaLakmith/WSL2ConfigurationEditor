using CLI.ConsoleInterface;
using CLI.States;

using Core.Abstractions.Configuration;

namespace CLI.InputHandlers;

public class StateHandlerImpl(IConfigurationIO configurationIO, IConfigurationState state, ILocalConfigurationState localState, IConsoleWritter writter) : IStateHandler
{
    private readonly IConfigurationIO _configurationIo = configurationIO;
    private readonly IConfigurationState _state = state;
    private readonly IConsoleWritter _writter = writter;
    private readonly ILocalConfigurationState _localState = localState;

    public async Task SaveChangesToFileAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            _localState.CommitChanges();
            await _configurationIo.SaveConfigurationToFileAsync(cancellationToken);
        }
        catch(IOException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    public async Task RefreshStateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            _state.ClearState();
            await _configurationIo.LoadConfigurationFromFileAsync(cancellationToken);
            _localState.ResetChanges();
        }
        catch(IOException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
        catch(InvalidDataException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    public Task ClearChangesAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _localState.ResetChanges();
        return Task.CompletedTask;
    }

    public async Task ResetToDefaultsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            _state.ClearState();
            _localState.ResetChanges();
            await _configurationIo.SaveConfigurationToFileAsync(cancellationToken);
        }
        catch(IOException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }
}
