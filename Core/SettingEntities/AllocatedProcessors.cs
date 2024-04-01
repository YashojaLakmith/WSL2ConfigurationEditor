using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"processors", SectionType.Common, @"Allocated Processors")]
    [SupportedWindowsVersion(10)]
    public class AllocatedProcessors : ISettingEntity
    {
        public int ProcessorCount { get; private set; }

        public AllocatedProcessors() { }

        public AllocatedProcessors(int processorCount)
        {
            ProcessorCount = processorCount;
        }

        public void SetValue(string valueAsString)
        {
            ProcessorCount = TryParseStringAsInt(valueAsString);
        }

        public string ParseValueAsString()
        {
            return ProcessorCount.ToString();
        }

        private static int TryParseStringAsInt(ReadOnlySpan<char> valueAsString)
        {
            if(uint.TryParse(valueAsString, out var result))
            {
                return (int) result;
            }

            throw new InvalidOperationException();
        }
    }
}
