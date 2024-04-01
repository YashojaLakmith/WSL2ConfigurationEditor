namespace Core.SettingEntities
{
    public class HostAddressLoopBack : BooleanSettingEntity, ISettingEntity
    {
        public HostAddressLoopBack() { }

        public HostAddressLoopBack(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
