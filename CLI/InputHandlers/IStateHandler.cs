
namespace CLI.InputHandlers;

public interface IStateHandler
{
    Task ClearChangesAsync(CancellationToken cancellationToken = default);
    Task RefreshStateAsync(CancellationToken cancellationToken = default);
    Task ResetToDefaultsAsync(CancellationToken cancellationToken = default);
    Task SaveChangesToFileAsync(CancellationToken cancellationToken = default);
}