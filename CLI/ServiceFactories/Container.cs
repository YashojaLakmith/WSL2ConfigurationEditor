using Microsoft.Extensions.DependencyInjection;

namespace CLI.ServiceFactories;

public class Container(IServiceProvider serviceProvider) : IServiceFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public T ResolveService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
