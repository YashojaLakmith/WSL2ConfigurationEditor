using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;

namespace CLI.InputHandlers;

public class InputParserImpl(IHelpHandler helpHandler, ISettingHandler settingHandler, IStateHandler stateHandler, IConsoleWritter consoleWritter) : IInputParser
{
    private readonly IHelpHandler _helpHandler = helpHandler;
    private readonly ISettingHandler _settingHandler = settingHandler;
    private readonly IStateHandler _stateHandler = stateHandler;
    private readonly IConsoleWritter _consoleWritter = consoleWritter;

    public async Task ParseInputAsync(string input, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await TryParseSingleCommandAsync(input, cancellationToken))
        {
            return;
        }

        await _consoleWritter.WriteStringAsync(@"Invalid command.", cancellationToken);
        await _helpHandler.DisplayGenericHelpAsync(cancellationToken);
    }

    private async Task<bool> TryParseSingleCommandAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await TryParseExitAsync(str, cancellationToken)
            || await TryParseAboutAsync(str, cancellationToken)
            || await TryParseHelpAsync(str, cancellationToken)
            || await TryParseListSettingsAsync(str, cancellationToken)
            || await TryParseMakeAllDefaultAsync(str, cancellationToken)
            || await TryParseRefreshAsync(str, cancellationToken)
            || await TryParseResetChangesAsync(str, cancellationToken)
            || await TryParseSaveSettingsAsync(str, cancellationToken)
            || await TryParseSettingInfoAsync(str, cancellationToken)
            || await TryParseMakeSingleDefaultAsync(str, cancellationToken)
            || await TryParseSetSettingAsync(str, cancellationToken);
    }

    private async Task<bool> TryParseExitAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"exit";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _helpHandler.HandleExitAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseHelpAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"help";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _helpHandler.DisplayGenericHelpAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseListSettingsAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"list-settings";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _settingHandler.ListAllSettingDataAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseSaveSettingsAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"save-changes";

        if (CaseInsensitiveStringCompare(str, text))
        {
            await _stateHandler.SaveChangesToFileAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseRefreshAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"refresh";

        if (CaseInsensitiveStringCompare(str, text))
        {
            await _stateHandler.RefreshStateAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseResetChangesAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"reset-changes";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _stateHandler.ClearChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseMakeAllDefaultAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"make-default-all";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _stateHandler.ResetToDefaultsAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseAboutAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"about";

        if (CaseInsensitiveStringCompare(text, str))
        {
            await _helpHandler.DisplayAboutAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<bool> TryParseSettingInfoAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"info ";

        if (!CaseInsensitiveStartsWith(str, text))
        {
            return false;
        }

        await _settingHandler.ListSingleSettingDataAsync(str[text.Length..], cancellationToken);
        return true;
    }

    private async Task<bool> TryParseMakeSingleDefaultAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"make-default ";

        if (!CaseInsensitiveStartsWith(str, text))
        {
            return false;
        }

        await _settingHandler.MakeDefaultAsync(str[text.Length..], cancellationToken);
        return true;
    }

    private async Task<bool> TryParseSetSettingAsync(string str, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        const string text = @"set ";

        return CaseInsensitiveStartsWith(str, text) && await TrySetSettingAsync(str[text.Length..], cancellationToken);
    }

    private static bool CaseInsensitiveStringCompare(string str1, string str2)
    {
        return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
    }

    private static bool CaseInsensitiveStartsWith(string str, string word)
    {
        return str.StartsWith(word, StringComparison.OrdinalIgnoreCase);
    }

    private async Task<bool> TrySetSettingAsync(string keyValueStr, CancellationToken cancellationToken = default)
    {
        try
        {
            var kvp = SplitToKeyValue(keyValueStr);
            await _settingHandler.SetSettingAsync(kvp.Key, kvp.Value, cancellationToken);
            return true;
        }
        catch (InvalidOperationException ex)
        {
            await _consoleWritter.WriteStringAsync(ex.Message, cancellationToken);
            return false;
        }
    }

    private static KeyValuePair<string, string> SplitToKeyValue(string str)
    {
        var parts = str.Split(' ');
        if (parts.Length != 2)
        {
            throw new InvalidOperationException(@"Invalid format.");
        }

        return KeyValuePair.Create(parts[0], parts[1]);
    }
}

/*
+list-settings                   Lists all settings.
+info <setting key>              Gives information on the setting with given key.
+save-changes                    Saves the changes to the .wslconfig file.
+refresh                         Reloads the configuration from the .wslconfig file.
+reset-changes                   Resets the unsaved changes.
+make-default <setting key>      Sets the given setting to its default.
+make-default-all                Sets the all settings to their defaults.
+set <setting key> <value>       Sets the setting to new value.
+about                           About WSL Configuration Editor.
+help                            Get help.
*/
