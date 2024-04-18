using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"dnsProxy", SectionType.Common, @"Only applicable to Networking Mode is set to NAT. Informs WSL to configure the DNS Server in Linux to the NAT on the host. Setting to false will mirror DNS servers from Windows to Linux.")]
    [SupportedWindowsVersion(10)]
    public class DNSProxy : BooleanSettingEntity, ISettingEntity
    {
        public DNSProxy(): base(true) { }

        public DNSProxy(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
