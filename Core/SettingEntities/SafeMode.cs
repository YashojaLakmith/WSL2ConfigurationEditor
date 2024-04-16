using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class SafeMode : BooleanSettingEntity, ISettingEntity
    {
        public SafeMode() : base(true) { }

        public SafeMode(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
