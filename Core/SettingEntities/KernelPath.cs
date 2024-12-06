using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"kernel", SectionType.Common, @"An absolute Windows path to a custom Linux kernel. By default, the Microsoft built kernel provided inbox.")]
[SupportedWindowsVersion(10)]
public class KernelPath : BaseDefaultableEntity, ISettingEntity
{
    public string PathToKernel { get; private set; }

    public KernelPath() : base(true)
    {
        PathToKernel = string.Empty;
    }

    public KernelPath(string path) : base(false)
    {
        ValidateWin32Path(path);
        PathToKernel = path;
    }

    public string ParseValueAsString()
    {
        return PathToKernel.Replace(@"\", @"\\");
    }

    public void SetValue(string valueAsString)
    {
        valueAsString = valueAsString.Replace(@"\\", @"\");
        ValidateWin32Path(valueAsString);
        PathToKernel = valueAsString;
        IsDefault &= false;
    }

    private static void ValidateWin32Path(string path)
    {
        string file;
        string? dir;

        if (string.IsNullOrWhiteSpace(path) || path.Equals(string.Empty))
        {
            throw new FormatException(@"Path should not be empty string or a string containing only whitespace characters.");
        }

        try
        {
            file = Path.GetFileName(path);
            dir = Path.GetDirectoryName(path);
        }
        catch (ArgumentException)
        {
            throw new FormatException(@"Path contains one or more invalid characters. Given path should be a valid Win32 path.");
        }
        catch (PathTooLongException)
        {
            throw new FormatException(@"Path contains characters more than the system defined limit.");
        }

        if (!Path.IsPathRooted(path))
        {
            throw new FormatException(@"Path should be an absolute Win32 path");

        }

        char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
        if (invalidFileNameChars.Intersect(file).Any() || path.Contains('/'))
        {
            throw new FormatException(@"File name contains invalid characters. File name should be a valid Win32 name.");
        }
    }
}
