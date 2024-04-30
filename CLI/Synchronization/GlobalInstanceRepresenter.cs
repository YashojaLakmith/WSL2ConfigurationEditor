using System.Diagnostics.CodeAnalysis;
using System.Security.AccessControl;
using System.Security.Principal;

namespace CLI.Synchronization;
public sealed class GlobalInstanceRepresenter : IDisposable
{
    private const string AppGuid = @"207BE061-F5A8-4413-A737-E0C930CF7422";
    private readonly Mutex _mutex;

    public static GlobalInstanceRepresenter Create()
    {
        return new();
    }

    private GlobalInstanceRepresenter()
    {
        _mutex = CreateAndConfigureMutex();
        TryAcquireMutex();
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static Mutex CreateAndConfigureMutex()
    {
        var name = $@"Global\{AppGuid}";
        var security = ConfigureMutexSecurity();
        var mutex = new Mutex(false, name, out _);
        mutex.SetAccessControl(security);

        return mutex;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static MutexSecurity ConfigureMutexSecurity()
    {
        var security = new MutexSecurity();
        var rule = CreateMutexAccessRule();
        security.AddAccessRule(rule);

        return security;
    }

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Apllication is exclusive to Windows platforms.")]
    private static MutexAccessRule CreateMutexAccessRule()
    {
        return new(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
            MutexRights.FullControl,
            AccessControlType.Allow);
    }

    private void TryAcquireMutex()
    {
        try
        {
            if (_mutex.WaitOne(1000, false))
            {
                return;
            }

            Console.WriteLine(@"An instance of the application is already running!. Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
        catch (AbandonedMutexException)
        {
            return;
        }
    }

    public void Dispose()
    {
        _mutex.ReleaseMutex();
        _mutex.Dispose();
    }
}
