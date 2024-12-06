using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"useWindowsDnsCache", SectionType.Experimental, @"Only applicable when DNS Tunneling is set to true. When this option is set to false, DNS requests tunneled from Linux will bypass cached names within Windows to always put the requests on the wire.")]
[SupportedWindowsVersion(10, 0, 19045, 1766)]
public class UseWindowsDNSCache : BooleanSettingEntity, ISettingEntity
{
    public UseWindowsDNSCache() : base(true) { }

    public UseWindowsDNSCache(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
