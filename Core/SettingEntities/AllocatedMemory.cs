using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"memory", SectionType.Common, @"AllocatedMemory")]
    [SupportedWindowsVersion(10)]
    public class AllocatedMemory : ISettingEntity
    {
        public int MemeoryInMegabytes { get; private set; }

        public AllocatedMemory() { }

        public AllocatedMemory(int value)
        {
            MemeoryInMegabytes = value;
        }

        public void SetValue(string valueAsString)
        {
            MemeoryInMegabytes = TryParseValue(valueAsString);
        }

        public string ParseValueAsString()
        {
            return $@"{MemeoryInMegabytes}MB";
        }

        private static int TryParseValue(ReadOnlySpan<char> valueStr)
        {
            var unitIdx = valueStr.Length - 2;
            var value = valueStr[..unitIdx];

            if(uint.TryParse(value, out var result))
            {
                return (int)result;
            }

            throw new InvalidOperationException();
        }
    }
}
