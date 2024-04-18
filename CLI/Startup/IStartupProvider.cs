
namespace CLI.Startup;

public interface IStartupProvider
{
    Task ConfigureStartupAsync(CancellationToken cancellationToken = default);
}