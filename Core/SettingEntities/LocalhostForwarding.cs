using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class LocalhostForwarding : BooleanSettingEntity, ISettingEntity
    {
        public LocalhostForwarding() : base(true) { }

        public LocalhostForwarding(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
