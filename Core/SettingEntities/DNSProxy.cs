using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class DNSProxy : BooleanSettingEntity, ISettingEntity
    {
        public DNSProxy(): base(true) { }

        public DNSProxy(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
