using CLI.Abstractions.ConsoleInterface;
using CLI.Abstractions.InputHandlers;
using CLI.Abstractions.Startup;
using CLI.Abstractions.States;
using CLI.ConsoleInterface;
using CLI.InputHandlers;
using CLI.Startup;
using CLI.States;

using Core.Abstractions.Attributes;
using Core.Abstractions.Configuration;
using Core.Abstractions.System;
using Core.Factories;

using Microsoft.Extensions.DependencyInjection;

namespace CLI.ServiceFactories;

public static class ServiceExtensions
{
    public static IServiceCollection AddAttributeExtractor(this IServiceCollection services)
    {
        return services.AddSingleton<IAttributeExtracter>(_ => AttributeExtractorFactory.CreateInstance());
    }

    public static IServiceCollection AddConfigurationIO(this IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationIO>(serviceProvider =>
        {
            IConfigurationState configurationState = serviceProvider.GetRequiredService<IConfigurationState>();
            IFileIO fileIo = serviceProvider.GetRequiredService<IFileIO>();

            return ConfigurationFactory.CreateCofigurationIO(configurationState, fileIo);
        });
    }

    public static IServiceCollection AddConfigurationState(this IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationState>(serviceProvider =>
        {
            IAttributeExtracter attributeExtractor = serviceProvider.GetRequiredService<IAttributeExtracter>();

            return ConfigurationFactory.CreateConfigurationState(attributeExtractor);
        });
    }

    public static IServiceCollection AddFileIO(this IServiceCollection services)
    {
        return services.AddSingleton<IFileIO>(_ => SystemInterfaceFactory.CreateFileIoInstance());
    }

    public static IServiceCollection AddSystemInterfaces(this IServiceCollection services)
    {
        return services.AddSingleton<ISystemInterfaces>(_ => SystemInterfaceFactory.CreateSystemInteface());
    }

    public static IServiceCollection AddConsoleWritter(this IServiceCollection services)
    {
        return services.AddSingleton<IConsoleWritter, ConsoleWritterImpl>();
    }

    public static IServiceCollection AddHelpHandler(this IServiceCollection services)
    {
        return services.AddSingleton<IHelpHandler, HelpHandlerImpl>();
    }

    public static IServiceCollection AddInputParser(this IServiceCollection services)
    {
        return services.AddSingleton<IInputParser, InputParserImpl>();
    }

    public static IServiceCollection AddSettingHandler(this IServiceCollection services)
    {
        return services.AddSingleton<ISettingHandler, SettingHandlerImpl>();
    }

    public static IServiceCollection AddLocalStateHandler(this IServiceCollection services)
    {
        return services.AddSingleton<IStateHandler, StateHandlerImpl>();
    }

    public static IServiceCollection AddAppLifetime(this IServiceCollection services)
    {
        return services.AddSingleton<IAppLifetime, AppLifetimeImpl>();
    }

    public static IServiceCollection AddStartupConfigurationProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IStartupProvider, StartupProviderImpl>();
    }

    public static IServiceCollection AddLocalConfigurationState(this IServiceCollection services)
    {
        return services.AddSingleton<ILocalConfigurationState, LocalConfigurationStateImpl>();
    }
}
