namespace Core.SettingEntities
{
    public class NestedVirtualization : BooleanSettingEntity, ISettingEntity
    {
        public NestedVirtualization() { }

        public NestedVirtualization(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
