using CLI.ConsoleInterface;
using CLI.InputHandlers;
using CLI.Startup;
using CLI.States;

using Core.Abstractions.Attributes;
using Core.Abstractions.Configuration;
using Core.Abstractions.System;
using Core.Attributes;
using Core.Configuration;
using Core.SystemInterfaces;

using Microsoft.Extensions.DependencyInjection;

namespace CLI.ServiceFactories;

public static class ServiceExtensions
{
    public static IServiceCollection AddAttributeExtractor(this IServiceCollection services)
    {
        return services.AddSingleton<IAttributeExtracter, AttributeExtracterImpl>();
    }

    public static IServiceCollection AddConfigurationIO(this IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationIO, ConfigurationIOImpl>();
    }

    public static IServiceCollection AddConfigurationState(this IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationState, ConfigurationStateImpl>();
    }

    public static IServiceCollection AddFileIO(this IServiceCollection services)
    {
        return services.AddSingleton<IFileIO, FileIOImpl>();
    }

    public static IServiceCollection AddSystemInterfaces(this IServiceCollection services)
    {
        return services.AddSingleton<ISystemInterfaces, SystemInterfacesImpl>();
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
        return services.AddSingleton<ISettingHandler,  SettingHandlerImpl>();
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
