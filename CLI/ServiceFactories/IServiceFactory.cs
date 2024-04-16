namespace CLI.ServiceFactories;

public interface IServiceFactory
{
    T ResolveService<T>() where T : notnull;
}
