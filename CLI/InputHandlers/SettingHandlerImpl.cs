using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;
using CLI.Abstractions.States;
using CLI.DTO;
using CLI.Extensions;

using Core.Exceptions;

namespace CLI.InputHandlers;

public class SettingHandlerImpl : ISettingHandler
{
    private readonly ILocalConfigurationState _localState;
    private readonly IConsoleWritter _writter;

    public SettingHandlerImpl(ILocalConfigurationState localSate, IConsoleWritter writter)
    {
        _localState = localSate;
        _writter = writter;
    }

    public async Task ListAllSettingDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            List<EntityData> settings = _localState.GetAllSettingData();
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
            EntityData data = _localState.GetSettingByKey(key).AsEntityData();
            string str = FormatString(data);
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
            Core.Abstractions.Entity.ISettingEntity setting = _localState.GetSettingByKey(key);
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
            Core.Abstractions.Entity.ISettingEntity entity = _localState.GetSettingByKey(key);
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
