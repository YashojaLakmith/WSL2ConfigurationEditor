namespace Core.Configuration;

public interface IConfigurationIO
{
    Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default);
    bool VerifyWslConfigExistence();
    Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default);
}
