using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"autoProxy", SectionType.Common, @"Enforces WSL to use Windows’ HTTP proxy information.")]
    [SupportedWindowsVersion(10, 0, 22000, 194)]
    public class AutoProxy : BooleanSettingEntity, ISettingEntity
    {
        public AutoProxy(): base(true) { }

        public AutoProxy(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
