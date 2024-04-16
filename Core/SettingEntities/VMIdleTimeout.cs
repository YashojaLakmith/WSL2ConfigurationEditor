using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class VMIdleTimeout : BaseDefaultableEntity, ISettingEntity
    {
        public uint TimeoutInMiliSeconds { get; private set; }

        public VMIdleTimeout() : base(true) { }

        public VMIdleTimeout(uint timeoutInMiliSeconds) : base(false)
        {
            TimeoutInMiliSeconds = timeoutInMiliSeconds;
        }

        public string ParseValueAsString()
        {
            return TimeoutInMiliSeconds.ToString();
        }

        public void SetValue(string valueAsString)
        {
            if (uint.TryParse(valueAsString, out var result))
            {
                TimeoutInMiliSeconds = result;
                IsDefault &= false;
                return;
            }

            throw new FormatException(@"Value must be a unsigned numerical value.");
        }
    }
}
