using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"bestEffortDnsParsing", SectionType.Experimental, @"Only applicable when DNS Tunneling is enabled. When enabled, Windows will extract the question from the DNS request and attempt to resolve it, ignoring the unknown records.")]
    [SupportedWindowsVersion(10, 0, 19045, 2251)]
    public class BestEffortDNSParsing : BooleanSettingEntity, ISettingEntity
    {
        public BestEffortDNSParsing(): base(true) { }

        public BestEffortDNSParsing(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
