namespace CLI.ServiceFactories;

/// <summary>
/// Defines methods for resolving services from the IoC provider.
/// </summary>
public interface IServiceFactory
{
    /// <summary>
    /// Resolves the requested service. Throws if failed.
    /// </summary>
    /// <returns>The requested service.</returns>
    T ResolveService<T>() where T : notnull;
}
