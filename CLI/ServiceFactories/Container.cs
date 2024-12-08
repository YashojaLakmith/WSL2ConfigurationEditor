using CLI.Abstractions.ServiceFactories;

using Microsoft.Extensions.DependencyInjection;

namespace CLI.ServiceFactories;

public class Container : IServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public Container(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T ResolveService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
