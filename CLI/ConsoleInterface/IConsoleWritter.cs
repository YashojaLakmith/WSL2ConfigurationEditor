using CLI.DTO;

namespace CLI.ConsoleInterface;
public interface IConsoleWritter
{
    Task WriteStringAsync(string str, CancellationToken cancellationToken = default);
    Task WriteExceptionAsync(Exception ex, CancellationToken cancellationToken = default);
    Task WriteTableAsync(IEnumerable<EntityData> entityData, CancellationToken cancellationToken = default);
}