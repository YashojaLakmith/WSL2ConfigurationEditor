using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"hostAddressLoopback", SectionType.Experimental, @"Only applicable when Networking Mode is set to Mirrored. When set to True, will allow the Container to connect to the Host, or the Host to connect to the Container, by an IP address that's assigned to the Host. The 127.0.0.1 loopback address can always be used,this option allows for all additionally assigned local IP addresses to be used as well. Only IPv4 addresses assigned to the host are supported.")]
[SupportedWindowsVersion(10, 0, 22621, 521)]
public class HostAddressLoopBack : BooleanSettingEntity, ISettingEntity
{
    public HostAddressLoopBack() : base(true) { }

    public HostAddressLoopBack(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
