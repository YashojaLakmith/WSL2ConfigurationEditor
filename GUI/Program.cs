using System.Security.AccessControl;
using System.Security.Principal;

using GUI.Extensions;
using GUI.Forms;
using GUI.ServiceFactories;

using Microsoft.Extensions.Hosting;

namespace GUI;

public static class Program
{
    [STAThread]
    static void Main()
    {
        using var mutex = CreateAndConfigureMutex();
        if(!TryAcquireMutex(mutex))
        {
            return;
        }

        TryInitializeApplication(mutex);
    }

    private static Mutex CreateAndConfigureMutex()
    {
        var name = CreateMutexName();
        var security = CreateAndConfigureMutexSecurity();
        var mutex = new Mutex(false, name, out _);
        mutex.SetAccessControl(security);

        return mutex;
    }

    private static string CreateMutexName()
    {
        return @"Global\75A41557-447D-4231-8B73-944EBADA1ACA";
    }

    private static MutexSecurity CreateAndConfigureMutexSecurity()
    {
        var mutexSecurity = new MutexSecurity();
        var accessRule = CreateMutexAccessRule();

        mutexSecurity.AddAccessRule(accessRule);

        return mutexSecurity;
    }

    private static MutexAccessRule CreateMutexAccessRule()
    {
        return new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            MutexRights.FullControl,
            AccessControlType.Allow);
    }

    private static bool TryAcquireMutex(Mutex mutex)
    {
        try
        {
            if (mutex.WaitOne(1000, false)) return true;

            MessageBox.Show(@"An instance of the application is already running.", @"NCryptor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        catch (AbandonedMutexException)
        {
            return true;
        }
    }

    private static void TryInitializeApplication(Mutex namedMutex)
    {
        try
        {
            BuildServiceFactory();
            ConfigureApplicationStartup();
            ExecuteMainWindow();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, @"An error occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            namedMutex.ReleaseMutex();
        }
    }

    private static void ConfigureApplicationStartup()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
    }

    private static void ExecuteMainWindow()
    {
        var mainWindow = ResolveMainWindow();
        Application.Run(mainWindow);
    }

    private static MainWindow ResolveMainWindow()
    {
        var serviceFactory = ServiceFactoryImpl.Create();
        return serviceFactory.ResolveService<MainWindow>();
    }

    private static ContainerBackedServiceFactory BuildServiceFactory()
    {
        var hostBuilder = Host.CreateDefaultBuilder();
        ConfigureServices(hostBuilder);
        var host = hostBuilder.Build();
        var serviceFactory = new ContainerBackedServiceFactory(host.Services);
        ServiceFactoryImpl.SetProvider(serviceFactory);

        return serviceFactory;
    }

    private static void ConfigureServices(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddMainWindow();
            services.AddAttributeExtractor();
            services.AddConfigurationState();
            services.AddConfigurationIO();
            services.AddFileIO();
            services.AddSystemInterfaces();
            services.AddStringOperations();
            services.AddValidator();
        });
    }
}