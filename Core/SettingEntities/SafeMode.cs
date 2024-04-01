namespace Core.SettingEntities
{
    public class SafeMode : BooleanSettingEntity, ISettingEntity
    {
        public SafeMode() { }

        public SafeMode(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
