using Microsoft.Extensions.DependencyInjection;

namespace GUI.ServiceFactories;

public class ContainerBackedServiceFactory(IServiceProvider serviceProvider) : IServiceFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public TService ResolveService<TService>() where TService : notnull
    {
        return _serviceProvider.GetRequiredService<TService>();
    }
}
