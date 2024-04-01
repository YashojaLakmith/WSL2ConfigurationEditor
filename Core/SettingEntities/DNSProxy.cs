namespace Core.SettingEntities
{
    public class DNSProxy : BooleanSettingEntity, ISettingEntity
    {
        public DNSProxy() { }

        public DNSProxy(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
