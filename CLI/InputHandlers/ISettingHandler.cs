
namespace CLI.InputHandlers;

public interface ISettingHandler
{
    Task ListAllSettingDataAsync(CancellationToken cancellationToken = default);
    Task ListSingleSettingDataAsync(string key, CancellationToken cancellationToken = default);
    Task SetSettingAsync(string key, string value, CancellationToken cancellationToken = default);
    Task MakeDefaultAsync(string key, CancellationToken cancellationToken = default);
}