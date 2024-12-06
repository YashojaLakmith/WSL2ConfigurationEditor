using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"guiApplications", SectionType.Common, @"Turns on or off support for GUI applications (WSLg) in WSL.")]
[SupportedWindowsVersion(10, 0, 22000, 194)]
public class GUIApplications : BooleanSettingEntity, ISettingEntity
{
    public GUIApplications() : base(true) { }

    public GUIApplications(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
