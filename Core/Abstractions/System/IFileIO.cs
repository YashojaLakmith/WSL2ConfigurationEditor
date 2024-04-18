namespace Core.Abstractions.System;

public interface IFileIO
{
    /// <summary>
    /// Asynchronously reads the text lines from the .wslconfig file.
    /// </summary>
    /// <returns>A <see cref="Task"/> containing the result of the operation.</returns>
    Task<string[]> ReadLinesFromFileAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously writes the given text lines to the .wslconfig file. Overwrites if the file already exists.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task WriteLinesToFileAsync(string[] lines, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously creates a empty .wslconfig file.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task CreateEmptyWslConfigFileAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies whether the .wslconfig file exists.
    /// </summary>
    /// <returns><c>true</c> if exists. Otherwise <c>false.</c></returns>
    bool CheckFileExistence();

    /// <summary>
    /// Verifies whether the application has permissios to access and modify the .wslconfig file.
    /// </summary>
    /// <returns><c>true</c> if has. Otherwise <c>false.</c></returns>
    bool ValidateFilePermissions();
}