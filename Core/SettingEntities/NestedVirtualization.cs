using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"nestedVirtualization", SectionType.Common, @"Boolean to turn on or off nested virtualization, enabling other nested VMs to run inside WSL 2.")]
    [SupportedWindowsVersion(10, 0, 22000, 194)]
    public class NestedVirtualization : BooleanSettingEntity, ISettingEntity
    {
        public NestedVirtualization() : base(true) { }

        public NestedVirtualization(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
