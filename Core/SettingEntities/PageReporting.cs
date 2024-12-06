using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"pageReporting", SectionType.Common, @"Enables or disables Windows to reclaim unused memory allocated to WSL 2 virtual machine.")]
[SupportedWindowsVersion(10)]
public class PageReporting : BooleanSettingEntity, ISettingEntity
{
    public PageReporting() : base(true) { }

    public PageReporting(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
