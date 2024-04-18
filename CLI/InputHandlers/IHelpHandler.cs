namespace CLI.InputHandlers;

/// <summary>
/// Defines methods for handling help related queries.
/// </summary>
public interface IHelpHandler
{
    /// <summary>
    /// Asynchronously displays generic help.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task DisplayGenericHelpAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously displays information about the application.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task DisplayAboutAsync(CancellationToken cancellationToken = default);
}