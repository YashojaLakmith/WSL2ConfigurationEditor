using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class NestedVirtualization : BooleanSettingEntity, ISettingEntity
    {
        public NestedVirtualization() : base(true) { }

        public NestedVirtualization(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
