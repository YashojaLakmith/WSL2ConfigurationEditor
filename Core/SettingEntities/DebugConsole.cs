using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class DebugConsole : BooleanSettingEntity, ISettingEntity
    {
        public DebugConsole(): base(true) { }

        public DebugConsole(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
