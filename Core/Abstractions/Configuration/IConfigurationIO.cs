namespace Core.Abstractions.Configuration;

public interface IConfigurationIO
{
    /// <exception cref="IOException"></exception>
    /// <exception cref="InvalidDataException"></exception>
    Task LoadConfigurationFromFileAsync(CancellationToken cancellationToken = default);

    bool VerifyWslConfigExistence();

    /// <exception cref="IOException"></exception>
    Task SaveConfigurationToFileAsync(CancellationToken cancellationToken = default);
}
