namespace GUI.ServiceFactories;

public interface IServiceFactory
{
    TService ResolveService<TService>() where TService : notnull;
}
