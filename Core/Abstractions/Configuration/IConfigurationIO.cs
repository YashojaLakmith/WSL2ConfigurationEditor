namespace Core.Abstractions.Configuration;

public interface IConfigurationIO
{
    /// <summary>
    /// Asynchronously loads the WSL2 configuration to the in-memory state from the .wslconfig file.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing th operation.</returns>
    Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies whether the .wslconfig file already exists.
    /// </summary>
    /// <returns><c>true</c> if exists. Otherwise <c>false.</c></returns>
    bool VerifyWslConfigExistence();

    /// <summary>
    /// Asynchronously writes the in-memory state of WSL2 configuration to the .wslconfig file, overwriting the existing content.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing th operation.</returns>
    Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default);
}
