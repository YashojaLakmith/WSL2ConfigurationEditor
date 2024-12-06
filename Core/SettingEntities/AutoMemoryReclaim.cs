using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"autoMemoryReclaim", SectionType.Experimental, @"Mode of automatic release of cached memory.")]
[SupportedWindowsVersion(10)]
public class AutoMemoryReclaim : BaseDefaultableEntity, ISettingEntity
{
    public MemoryReclaimType ReclaimType { get; private set; }

    public AutoMemoryReclaim() : base(true) { }

    public AutoMemoryReclaim(MemoryReclaimType reclaimType) : base(false)
    {
        ReclaimType = reclaimType;
    }

    public void SetValue(string valueAsString)
    {
        ReclaimType = MatchType(valueAsString);
        IsDefault &= false;
    }

    public string ParseValueAsString()
    {
        return ReclaimType.ToString().ToLower();
    }

    private static MemoryReclaimType MatchType(string valueAsString)
    {
        return CheckEquality(MemoryReclaimType.Gradual, valueAsString)
            ? MemoryReclaimType.Gradual
            : CheckEquality(MemoryReclaimType.DropCache, valueAsString)
            ? MemoryReclaimType.DropCache
            : throw new FormatException(@"Value should either be 'gradual' or 'dropcache'");
    }

    private static bool CheckEquality(MemoryReclaimType type, string valueAsString)
    {
        return type.ToString()
            .Equals(valueAsString, StringComparison.OrdinalIgnoreCase);
    }
}
