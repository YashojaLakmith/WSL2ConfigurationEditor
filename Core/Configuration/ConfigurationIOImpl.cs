using Core.Abstractions.Configuration;
using Core.Abstractions.System;

namespace Core.Configuration;

public class ConfigurationIOImpl(IConfigurationState state, IFileIO fileIO) : IConfigurationIO
{
    private readonly IConfigurationState _state = state;
    private readonly IFileIO _fileIO = fileIO;

    public async Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default)     // Needs to handle exceptions
    {
        var lines = await _fileIO.ReadLinesFromFileAsync(cancellationToken);
        VerifyWsl2Tag(lines);
        var idx = FindIndexOfWsl2Tag(lines);
        _state.LoadConfiguration(lines.Skip(idx + 1));
    }

    public async Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default)       // Needs to handle exceptions
    {
        IEnumerable<string> lines = _state.ParseAsConfigLines();
        try
        {
            VerifyWsl2Tag(lines);
        }
        catch (InvalidDataException)
        {
            lines = lines.Prepend(@"[wsl2]");
        }

        await _fileIO.WriteLinesToFileAsync(lines.ToArray(), cancellationToken);
    }

    public bool VerifyWslConfigExistence()
    {
        return _fileIO.CheckFileExistence();
    }

    private static void VerifyWsl2Tag(IEnumerable<string> lines)
    {
        if(!lines.Any())
        {
            throw new InvalidDataException(@"File does not conatain valid .wslconfig data.");
        }

        foreach(var line in lines)
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

    private static int FindIndexOfWsl2Tag(IEnumerable<string> lines)
    {
        var i = 0;
        foreach (var item in lines)
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
