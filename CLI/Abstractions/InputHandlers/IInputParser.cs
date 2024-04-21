namespace CLI.Abstractions.InputHandlers;

/// <summary>
/// Defines methods for parsing the user text input and invoking the necessary services.
/// </summary>
public interface IInputParser
{
    /// <summary>
    /// Asynchronously parses the given text input and submits it to the relevant service provider.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the operation.</returns>
    Task ParseInputAsync(string input, CancellationToken cancellationToken = default);
}