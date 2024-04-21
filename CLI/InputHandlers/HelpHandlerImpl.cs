using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;

namespace CLI.InputHandlers;

public class HelpHandlerImpl(IConsoleWritter consoleWritter) : IHelpHandler
{
    private readonly IConsoleWritter _consoleWritter = consoleWritter;

    public async Task DisplayGenericHelpAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var text = SettingHelpText();
        await _consoleWritter.WriteStringAsync(text, cancellationToken);
    }

    public async Task DisplayAboutAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var text = AboutText();
        await _consoleWritter.WriteStringAsync(text, cancellationToken);
    }

    private static string SettingHelpText()
    {
        return @"
list-settings                   Lists all settings.
info <setting key>              Gives information on the setting with given key.
save-changes                    Saves the changes to the .wslconfig file.
refresh                         Reloads the configuration from the .wslconfig file.
reset-changes                   Resets the unsaved changes.
make-default <setting key>      Sets the given setting to its default.
make-default-all                Sets the all settings to their defaults.
set <setting key> <value>       Sets the setting to new value.
about                           About WSL Configuration Editor.
help                            Get help.
";
    }

    private static string AboutText()
    {
        return @"
WSL Configuration Editor Version 1.0
https://github.com/YashojaLakmith/WSL2ConfigurationEditor
";
    }
}
