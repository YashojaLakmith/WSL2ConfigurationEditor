using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"safeMode", SectionType.Common, @"Run WSL in ""Safe Mode"" which disables many features and is intended to be used to recover distributions that are in bad states.")]
    [SupportedWindowsVersion(10, 0, 22000, 194)]
    public class SafeMode : BooleanSettingEntity, ISettingEntity
    {
        public SafeMode() : base(true) { }

        public SafeMode(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
