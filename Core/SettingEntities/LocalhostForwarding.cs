using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"localhostForwarding", SectionType.Common, @"Boolean specifying if ports bound to wildcard or localhost in the WSL 2 VM should be connectable from the host via localhost:port.")]
    [SupportedWindowsVersion(10)]
    public class LocalhostForwarding : BooleanSettingEntity, ISettingEntity
    {
        public LocalhostForwarding() : base(true) { }

        public LocalhostForwarding(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
