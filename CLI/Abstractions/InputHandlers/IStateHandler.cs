namespace CLI.Abstractions.InputHandlers;

/// <summary>
/// Defines methods for manipulating the in-memory state of the configuration.
/// </summary>
public interface IStateHandler
{
    /// <summary>
    /// Asynchronously clears any non-commited changes from the local state.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ClearChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously refreshes the local in-memory state by re-reading from the configuration file.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task RefreshStateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously sets all the settings in the local state to their defaults.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ResetToDefaultsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously saves the in-memory state to the disk overwriting the existing configuration.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task SaveChangesToFileAsync(CancellationToken cancellationToken = default);
}