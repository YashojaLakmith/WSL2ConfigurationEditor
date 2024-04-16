
namespace CLI.InputHandlers;

public interface IInputParser
{
    Task ParseInputAsync(string input, CancellationToken cancellationToken = default);
}