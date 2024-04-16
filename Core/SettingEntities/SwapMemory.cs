using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class SwapMemory : BaseDefaultableEntity, ISettingEntity
    {
        public uint SwapMemoryInMegaBytes { get; private set; }

        public SwapMemory() : base(true) { }

        public SwapMemory(uint swapMemoryInMegaBytes) : base(false)
        {
            SwapMemoryInMegaBytes = swapMemoryInMegaBytes;
        }

        public string ParseValueAsString()
        {
            return $@"{SwapMemoryInMegaBytes}MB";
        }

        public void SetValue(string valueAsString)
        {
            if(uint.TryParse(valueAsString, out var result))
            {
                SwapMemoryInMegaBytes = result;
                IsDefault &= false;
                return;
            }

            throw new FormatException(@"Value must be a unsigned numerical value.");
        }
    }
}
