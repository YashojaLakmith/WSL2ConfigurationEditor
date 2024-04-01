namespace Core.SettingEntities
{
    public class PageReporting : BooleanSettingEntity, ISettingEntity
    {
        public PageReporting() { }

        public PageReporting(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
