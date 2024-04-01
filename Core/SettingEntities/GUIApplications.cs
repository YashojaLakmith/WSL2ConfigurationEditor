namespace Core.SettingEntities
{
    public class GUIApplications : BooleanSettingEntity, ISettingEntity
    {
        public GUIApplications() { }

        public GUIApplications(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
