using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
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
