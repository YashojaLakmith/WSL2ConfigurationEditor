namespace CLI.ServiceFactories;

public class DefaultServiceFactory
{
    private static IServiceFactory? _serviceFactory;

    private DefaultServiceFactory() { }

    public static void SetProvider(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public static T ResolveService<T>() where T : notnull
    {
        if(_serviceFactory is not null)
        {
            return _serviceFactory.ResolveService<T>();
        }

        throw new InvalidOperationException(@"Factory has not yet been built.");
    }
}
