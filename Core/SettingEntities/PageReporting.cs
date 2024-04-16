using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class PageReporting : BooleanSettingEntity, ISettingEntity
    {
        public PageReporting() : base(true) { }

        public PageReporting(bool isEnabled) : base(false)
        {
            Value = isEnabled;
        }
    }
}
