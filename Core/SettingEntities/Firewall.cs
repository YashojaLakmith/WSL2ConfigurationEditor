using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"firewall", SectionType.Common, @"Setting this to true allows the Windows Firewall rules, as well as rules specific to Hyper-V traffic, to filter WSL network traffic.")]
[SupportedWindowsVersion(10, 0, 22621, 521)]
public class Firewall : BooleanSettingEntity, ISettingEntity
{
    public Firewall() : base(true) { }

    public Firewall(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
