
namespace CLI.Startup;

/// <summary>
/// Defines methods for prepare the application for user interaction.
/// </summary>
public interface IStartupProvider
{
    /// <summary>
    /// Asynchronously loads and configures all the required resources before proceeding to interact with user.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ConfigureStartupAsync(CancellationToken cancellationToken = default);
}