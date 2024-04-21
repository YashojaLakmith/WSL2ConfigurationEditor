using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.Startup;
using CLI.ServiceFactories;

using Microsoft.Extensions.Hosting;

namespace CLI;

public class Program
{
    public static async Task Main()
    {
        BuildServiceFactory();
        await ConfigureStartupAsync();
        await WriteGreetingAsync();
        await RunMessageLoopAsync();
    }

    private static void BuildServiceFactory()
    {
        var hostBuilder = Host.CreateDefaultBuilder();
        ConfigureServices(hostBuilder);
        var host = hostBuilder.Build();
        var container = new Container(host.Services);
        DefaultServiceFactory.SetProvider(container);
    }

    private static void ConfigureServices(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddAttributeExtractor();
            services.AddConfigurationState();
            services.AddConfigurationIO();
            services.AddFileIO();
            services.AddSystemInterfaces();
            services.AddConsoleWritter();
            services.AddHelpHandler();
            services.AddInputParser();
            services.AddSettingHandler();
            services.AddLocalStateHandler();
            services.AddAppLifetime();
            services.AddStartupConfigurationProvider();
            services.AddLocalConfigurationState();
        });
    }

    private static async Task WriteGreetingAsync()
    {
        var writer = DefaultServiceFactory.ResolveService<IConsoleWritter>();
        var msg = "WSL Configuration Editor\nInsert \"help\" for get all commands.";

        await writer.WriteStringAsync(msg);
    }

    private static async Task ConfigureStartupAsync()
    {
        var startup = DefaultServiceFactory.ResolveService<IStartupProvider>();
        await startup.ConfigureStartupAsync();
    }

    private static async Task RunMessageLoopAsync()
    {
        var lifeTime = DefaultServiceFactory.ResolveService<IAppLifetime>();
        await lifeTime.StartLoopAsync();
    }
}
