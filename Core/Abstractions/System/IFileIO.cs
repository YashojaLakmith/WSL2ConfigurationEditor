namespace Core.Abstractions.System;

public interface IFileIO
{
    /// <exception cref="IOException"></exception>
    Task<string[]> ReadLinesFromFileAsync(CancellationToken cancellationToken = default);

    /// <exception cref="IOException"></exception>
    Task WriteLinesToFileAsync(string[] lines, CancellationToken cancellationToken = default);

    /// <exception cref="IOException"></exception>
    Task CreateEmptyWslConfigFileAsync(CancellationToken cancellationToken = default);

    bool CheckFileExistence();
    bool ValidateFilePermissions();
}