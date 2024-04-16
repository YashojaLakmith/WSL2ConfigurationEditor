using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class Firewall : BooleanSettingEntity, ISettingEntity
    {
        public Firewall() : base(true) { }

        public Firewall(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
