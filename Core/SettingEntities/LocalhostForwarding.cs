
namespace Core.SettingEntities
{
    public class LocalhostForwarding : BooleanSettingEntity, ISettingEntity
    {
        public LocalhostForwarding() { }

        public LocalhostForwarding(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
