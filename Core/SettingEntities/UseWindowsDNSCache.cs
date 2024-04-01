namespace Core.SettingEntities
{
    public class UseWindowsDNSCache : BooleanSettingEntity, ISettingEntity
    {
        public UseWindowsDNSCache() { }

        public UseWindowsDNSCache(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
