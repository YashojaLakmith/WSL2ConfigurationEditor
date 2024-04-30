﻿using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.Startup;
using CLI.ServiceFactories;
using CLI.Synchronization;

using Microsoft.Extensions.DependencyInjection;

namespace CLI;

public class Program
{
    public static async Task Main()
    {
        using var instance = GlobalInstanceRepresenter.Create();
        BuildServiceFactory();
        await ConfigureStartupAsync();
        await WriteGreetingAsync();
        await RunMessageLoopAsync();
    }

    private static void BuildServiceFactory()
    {
        DefaultServiceFactory.BuildServiceFactory(ConfigureServices);
    }

    private static void ConfigureServices(IServiceCollection services)
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
