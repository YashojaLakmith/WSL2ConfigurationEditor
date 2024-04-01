namespace GUI.ServiceFactories;

public class ServiceFactoryImpl : IServiceFactory
{
    private static IServiceFactory _provider;

    public static ServiceFactoryImpl Create()
    {
        return new();
    }

    public static void SetProvider(IServiceFactory provider)
    {
        _provider = provider;
    }

    public TService ResolveService<TService>() where TService : notnull
    {
        return _provider.ResolveService<TService>();
    }

    private ServiceFactoryImpl() { }
}
