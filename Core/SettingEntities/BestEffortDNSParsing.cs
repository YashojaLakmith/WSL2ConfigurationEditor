namespace Core.SettingEntities
{
    public class BestEffortDNSParsing : BooleanSettingEntity, ISettingEntity
    {
        public BestEffortDNSParsing() { }

        public BestEffortDNSParsing(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
