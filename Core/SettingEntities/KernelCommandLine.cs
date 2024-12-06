using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"kernelCommandLine", SectionType.Common, @"Additional command line aruments.")]
[SupportedWindowsVersion(10)]
public class KernelCommandLine : BaseDefaultableEntity, ISettingEntity
{
    public string CommandLineArgs { get; private set; }

    public KernelCommandLine() : base(true)
    {
        CommandLineArgs = string.Empty;
    }

    public KernelCommandLine(string commandLineArgs) : base(false)
    {
        CommandLineArgs = commandLineArgs;
    }

    public string ParseValueAsString()
    {
        return CommandLineArgs;
    }

    public void SetValue(string valueAsString)
    {
        CommandLineArgs = valueAsString;
        IsDefault &= false;
    }
}
