using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class BestEffortDNSParsing : BooleanSettingEntity, ISettingEntity
    {
        public BestEffortDNSParsing(): base(true) { }

        public BestEffortDNSParsing(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
