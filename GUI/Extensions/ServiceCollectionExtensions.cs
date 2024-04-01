using Core.Attributes;
using Core.Configuration;
using Core.Parsers;
using Core.SystemInterfaces;

using GUI.Forms;

using Microsoft.Extensions.DependencyInjection;

using WSL2ConfigurationEditor.Core.Validations;

namespace GUI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMainWindow(this IServiceCollection services)
    {
        return services.AddScoped<MainWindow>();
    }

    public static IServiceCollection AddAttributeExtractor(this IServiceCollection services)
    {
        return services.AddSingleton<IAttributeExtracter, AttributeExtracterImpl>();
    }

    public static IServiceCollection AddConfigurationState(this IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationState, ConfigurationStateImpl>();
    }

    public static IServiceCollection AddConfigurationIO(this  IServiceCollection services)
    {
        return services.AddSingleton<IConfigurationIO, ConfigurationIOImpl>();
    }

    public static IServiceCollection AddFileIO(this IServiceCollection services)
    {
        return services.AddSingleton<IFileIO, FileIOImpl>();
    }

    public static IServiceCollection AddSystemInterfaces(this IServiceCollection services)
    {
        return services.AddSingleton<ISystemInterfaces, SystemInterfacesImpl>();
    }

    public static IServiceCollection AddStringOperations(this IServiceCollection services)
    {
        return services.AddSingleton<IStringOperations, StringOperationsImpl>();
    }

    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        return services.AddSingleton<IValidator, ValidatorImpl>();
    }
}
