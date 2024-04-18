using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"debugConsole", SectionType.Common, @"Boolean to turn on an output console Window that shows the contents of dmesg upon start of a WSL 2 distro instance.")]
    [SupportedWindowsVersion(10, 0, 22000, 194)]
    public class DebugConsole : BooleanSettingEntity, ISettingEntity
    {
        public DebugConsole(): base(true) { }

        public DebugConsole(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
