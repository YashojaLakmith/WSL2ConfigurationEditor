using CLI.DTO;

namespace CLI.Abstractions.ConsoleInterface;

/// <summary>
/// Defines methods for writing to the console.
/// </summary>
public interface IConsoleWritter
{
    Task WriteStringAsync(string str, CancellationToken cancellationToken = default);
    Task WriteExceptionAsync(Exception ex, CancellationToken cancellationToken = default);
    Task WriteTableAsync(IReadOnlyCollection<EntityData> entityData, CancellationToken cancellationToken = default);
}