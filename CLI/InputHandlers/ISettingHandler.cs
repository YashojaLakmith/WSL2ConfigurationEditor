
namespace CLI.InputHandlers;

/// <summary>
/// Defines methods for handling setting related queries.
/// </summary>
public interface ISettingHandler
{
    /// <summary>
    /// Asynchronously lists all the settings supported by the application.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ListAllSettingDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously provides detailed information about the given single setting.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ListSingleSettingDataAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously sets the value of a setting with the given key.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously sets a given setting to its default value.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task MakeDefaultAsync(string key, CancellationToken cancellationToken = default);
}