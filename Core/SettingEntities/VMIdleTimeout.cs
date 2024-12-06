using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"vmIdleTimeout", SectionType.Common, @"The number of milliseconds that a VM is idle, before it is shut down.")]
[SupportedWindowsVersion(10, 0, 22000, 194)]
public class VMIdleTimeout : BaseDefaultableEntity, ISettingEntity
{
    public uint TimeoutInMiliSeconds { get; private set; }

    public VMIdleTimeout() : base(true) { }

    public VMIdleTimeout(uint timeoutInMiliSeconds) : base(false)
    {
        TimeoutInMiliSeconds = timeoutInMiliSeconds;
    }

    public string ParseValueAsString()
    {
        return TimeoutInMiliSeconds.ToString();
    }

    public void SetValue(string valueAsString)
    {
        if (uint.TryParse(valueAsString, out uint result))
        {
            TimeoutInMiliSeconds = result;
            IsDefault &= false;
            return;
        }

        throw new FormatException(@"Value must be a unsigned numerical value.");
    }
}
