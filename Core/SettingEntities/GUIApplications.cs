using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class GUIApplications : BooleanSettingEntity, ISettingEntity
    {
        public GUIApplications() : base(true) { }

        public GUIApplications(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
