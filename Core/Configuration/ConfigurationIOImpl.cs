using Core.Abstractions.Configuration;
using Core.Abstractions.System;

namespace Core.Configuration;

internal class ConfigurationIOImpl : IConfigurationIO
{
    private readonly IConfigurationState _state;
    private readonly IFileIO _fileIO;

    public ConfigurationIOImpl(IConfigurationState state, IFileIO fileIO)
    {
        _state = state;
        _fileIO = fileIO;
    }

    public async Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default)
    {
        string[] lines = await _fileIO.ReadLinesFromFileAsync(cancellationToken);
        VerifyWsl2Tag(lines);
        int idx = FindIndexOfWsl2Tag(lines);
        _state.LoadConfiguration(lines.Skip(idx + 1));
    }

    public async Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default)
    {
        List<string> lines = _state.ParseAsConfigLines();
        try
        {
            VerifyWsl2Tag(lines);
        }
        catch (InvalidDataException)
        {
            lines.Insert(0, @"[wsl2]");
        }

        await _fileIO.WriteLinesToFileAsync([.. lines], cancellationToken);
    }

    public bool VerifyWslConfigExistence()
    {
        return _fileIO.CheckFileExistence();
    }

    private static void VerifyWsl2Tag(IReadOnlyCollection<string> lines)
    {
        if (lines.Count == 0)
        {
            throw new InvalidDataException(@"File does not conatain valid .wslconfig data.");
        }

        foreach (string line in lines)
        {
            if (line.Equals(string.Empty))
            {
                continue;
            }

            if (line.Equals(@"[wsl2]"))
            {
                return;
            }

            throw new InvalidDataException(@"File does not conatain valid .wslconfig data.");
        }
    }

    private static int FindIndexOfWsl2Tag(IReadOnlyCollection<string> lines)
    {
        int i = 0;
        foreach (string item in lines)
        {
            if (item.Equals(@"[wsl2]"))
            {
                break;
            }

            i++;
        }

        return i;
    }
}
