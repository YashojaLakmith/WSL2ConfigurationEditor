namespace CLI.Startup;

public interface IAppLifetime
{
    Task StartLoopAsync();
}