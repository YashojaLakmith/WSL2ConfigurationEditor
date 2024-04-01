using Core.SystemInterfaces;

namespace Core.Configuration;

public class ConfigurationIOImpl(IConfigurationState state, IFileIO fileIO) : IConfigurationIO
{
    private readonly IConfigurationState _state = state;
    private readonly IFileIO _fileIO = fileIO;

    public async Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default)
    {
        var lines = await _fileIO.ReadLinesFromFileAsync(cancellationToken);
        VerifyWsl2Tag(lines);
        _state.LoadConfiguration(lines);
    }

    public async Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default)
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
            throw new InvalidDataException();
        }

        foreach(var line in lines)
        {
            if (line.Equals(string.Empty))
            {
                continue;
            }

            if (!line.Equals(@"[wsl2]"))
            {
                throw new InvalidDataException();
            }
        }
    }
}
