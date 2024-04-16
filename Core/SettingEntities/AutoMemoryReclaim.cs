using Core.Abstractions.Entity;
using Core.Enumerations;

namespace Core.SettingEntities
{
    public class AutoMemoryReclaim : BaseDefaultableEntity, ISettingEntity
    {
        public MemoryReclaimType ReclaimType { get; private set; }

        public AutoMemoryReclaim(): base(true) { }

        public AutoMemoryReclaim(MemoryReclaimType reclaimType): base(false)
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
            return nameof(ReclaimType).ToLower();
        }

        private static MemoryReclaimType MatchType(string valueAsString)
        {
            if (CheckEquality(MemoryReclaimType.Gradual, valueAsString))
            {
                return MemoryReclaimType.Gradual;
            }

            if (CheckEquality(MemoryReclaimType.DropCache, valueAsString))
            {
                return MemoryReclaimType.DropCache;
            }

            throw new FormatException(@"Value should either be 'gradual' or 'dropcache'");
        }

        private static bool CheckEquality(MemoryReclaimType type, string  valueAsString)
        {
            return type.ToString()
                .Equals(valueAsString, StringComparison.OrdinalIgnoreCase);
        }
    }
}
