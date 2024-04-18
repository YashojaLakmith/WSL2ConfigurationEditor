namespace CLI.Startup;

/// <summary>
/// Defines top level services responsible for handling the application lifetime.
/// </summary>
public interface IAppLifetime
{
    /// <summary>
    /// Asynchronously starts the loop for user inputs and submits the inputs to the necessary services untill the application is terminated.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task StartLoopAsync();
}