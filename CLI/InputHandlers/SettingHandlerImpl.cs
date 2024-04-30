using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;
using CLI.Abstractions.States;
using CLI.DTO;
using CLI.Extensions;

using Core.Exceptions;

namespace CLI.InputHandlers;

public class SettingHandlerImpl(ILocalConfigurationState localSate, IConsoleWritter writter) : ISettingHandler
{
    private readonly ILocalConfigurationState _localState = localSate;
    private readonly IConsoleWritter _writter = writter;

    public async Task ListAllSettingDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var settings = _localState.GetAllSettingData();
            await _writter.WriteTableAsync(settings, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    public async Task ListSingleSettingDataAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var data = _localState.GetSettingByKey(key).AsEntityData();
            var str = FormatString(data);
            await _writter.WriteStringAsync(str, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    public async Task MakeDefaultAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var setting = _localState.GetSettingByKey(key);
            setting.SetDefaultValue();
        }
        catch (InvalidOperationException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    public async Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = _localState.GetSettingByKey(key);
            entity.SetValue(value);
            await _writter.WriteStringAsync(@"Done.", cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
        catch (FormatException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
        catch (PlatformInvokeException ex)
        {
            await _writter.WriteExceptionAsync(ex, cancellationToken);
        }
    }

    private static string FormatString(EntityData data)
    {
        return @$"
Setting Key:    {data.SettingId}
Setting Type:   {data.SectionName}
Current Value:  {data.SettingValue}
Is Supported:   {(data.IsSupported ? @"Yes" : @"No")}
Description:    {data.CommonName}
";
    }
}
