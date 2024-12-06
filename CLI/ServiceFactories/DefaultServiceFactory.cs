using CLI.Abstractions.ServiceFactories;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CLI.ServiceFactories;

public class DefaultServiceFactory
{
    private static IServiceFactory? _serviceFactory;

    private DefaultServiceFactory() { }

    public static T ResolveService<T>() where T : notnull
    {
        return _serviceFactory is not null
            ? _serviceFactory.ResolveService<T>()
            : throw new InvalidOperationException(@"Factory has not yet been built.");
    }

    public static void BuildServiceFactory(Action<IServiceCollection> serviceDelegate)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder();
        ConfigureServices(builder, serviceDelegate);
        IHost host = builder.Build();
        Container container = new(host.Services);
        SetProvider(container);
    }

    private static void ConfigureServices(IHostBuilder builder, Action<IServiceCollection> serviceDelegate)
    {
        builder.ConfigureServices((context, sevices) =>
        {
            serviceDelegate(sevices);
        });
    }

    private static void SetProvider(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
}
