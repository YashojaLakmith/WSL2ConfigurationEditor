using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class HostAddressLoopBack : BooleanSettingEntity, ISettingEntity
    {
        public HostAddressLoopBack() : base(true) { }

        public HostAddressLoopBack(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
