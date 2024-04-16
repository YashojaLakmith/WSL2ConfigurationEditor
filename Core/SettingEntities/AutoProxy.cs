using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class AutoProxy : BooleanSettingEntity, ISettingEntity
    {
        public AutoProxy(): base(true) { }

        public AutoProxy(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
