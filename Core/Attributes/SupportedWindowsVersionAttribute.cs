namespace Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SupportedWindowsVersionAttribute : Attribute
{
    private readonly int _major;
    private readonly int _minor;
    private readonly int _build;
    private readonly int _revision;

    public SupportedWindowsVersionAttribute(
        int major,
        int minor = 0,
        int build = 0,
        int revision = 0)
    {
        _major = major;
        _minor = minor;
        _build = build;
        _revision = revision;
    }

    public bool IsSupportedOnThisPlatform()
    {
        return OperatingSystem.IsWindowsVersionAtLeast(_major, _minor, _build, _revision);
    }
}
