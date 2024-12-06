using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"processors", SectionType.Common, @"Allocated processor Cores.")]
[SupportedWindowsVersion(10)]
public class AllocatedProcessors : BaseDefaultableEntity, ISettingEntity
{
    public uint ProcessorCount { get; private set; }

    public AllocatedProcessors() : base(true) { }

    public AllocatedProcessors(uint processorCount) : base(false)
    {
        ProcessorCount = processorCount;
    }

    public void SetValue(string valueAsString)
    {
        uint count = TryParseStringAsInt(valueAsString);
        ValidateProcessorCount(count);
        ProcessorCount = count;
        IsDefault &= false;
    }

    public string ParseValueAsString()
    {
        return ProcessorCount.ToString();
    }

    private static uint TryParseStringAsInt(ReadOnlySpan<char> valueAsString)
    {
        return uint.TryParse(valueAsString, out uint result)
            ? result
            : throw new FormatException(@"Value should be a unsigned numeric value.");
    }

    private static void ValidateProcessorCount(uint processorCount)
    {
        uint sysCount = GetProcessorCount();
        if (processorCount < 1)
        {
            throw new ArgumentOutOfRangeException(@"Processor count should not be less than 1");
        }

        if (processorCount > sysCount)
        {
            throw new ArgumentOutOfRangeException(@"Processor count should not be greater than available logical processors in the system.");
        }
    }

    private static uint GetProcessorCount()
    {
        return (uint)Environment.ProcessorCount;
    }
}
