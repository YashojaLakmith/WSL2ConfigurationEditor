using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class UseWindowsDNSCache : BooleanSettingEntity, ISettingEntity
    {
        public UseWindowsDNSCache() : base(true) { }

        public UseWindowsDNSCache(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
