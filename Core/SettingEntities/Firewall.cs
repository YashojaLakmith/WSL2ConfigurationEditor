namespace Core.SettingEntities
{
    public class Firewall : BooleanSettingEntity, ISettingEntity
    {
        public Firewall() { }

        public Firewall(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
