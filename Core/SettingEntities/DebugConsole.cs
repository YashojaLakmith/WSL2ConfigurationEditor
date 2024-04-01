namespace Core.SettingEntities
{
    public class DebugConsole : BooleanSettingEntity, ISettingEntity
    {
        public DebugConsole() { }

        public DebugConsole(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
