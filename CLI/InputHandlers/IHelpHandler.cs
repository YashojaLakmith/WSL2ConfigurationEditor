
namespace CLI.InputHandlers;

public interface IHelpHandler
{
    Task DisplayGenericHelpAsync(CancellationToken cancellationToken = default);
    Task DisplayAboutAsync(CancellationToken cancellationToken = default);
}