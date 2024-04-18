using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"initialAutoProxyTimeout", SectionType.Experimental, @"Only applicable when Auto Proxy is set to true. Configures how long (in milliseconds) WSL will wait for retrieving HTTP proxy information when starting a WSL container. If proxy settings are resolved after this time, the WSL instance must be restarted to use the retrieved proxy settings.")]
    [SupportedWindowsVersion(10, 0, 22000, 194)]
    public class InitialAutoProxyTimeout : BaseDefaultableEntity, ISettingEntity
    {
        public uint TimeoutMiliSeconds { get; private set; }

        public InitialAutoProxyTimeout() : base(true) { }

        public InitialAutoProxyTimeout(uint timeoutMiliSeconds) : base(false)
        {
            TimeoutMiliSeconds = timeoutMiliSeconds;
        }

        public string ParseValueAsString()
        {
            return TimeoutMiliSeconds.ToString();
        }

        public void SetValue(string valueAsString)
        {
            if(uint.TryParse(valueAsString, out var result))
            {
                TimeoutMiliSeconds = result;
                IsDefault &= false;
                return;
            }

            throw new FormatException(@"The value should be a valid milisecond value.");
        }
    }
}
