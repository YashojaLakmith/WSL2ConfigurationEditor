using Core.Enumerations;
using Core.Extensions;

namespace Core.SettingEntities
{
    public class AutoMemoryReclaim : ISettingEntity
    {
        public MemoryReclaimType ReclaimType { get; private set; }

        public AutoMemoryReclaim() { }

        public AutoMemoryReclaim(MemoryReclaimType reclaimType)
        {
            ReclaimType = reclaimType;
        }

        public void SetValue(string valueAsString)
        {
            ReclaimType = MatchType(valueAsString);
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

            throw new InvalidOperationException();
        }

        private static bool CheckEquality(MemoryReclaimType type, string  valueAsString)
        {
            return type.ToLowerCaseString().Equals(valueAsString);
        }
    }
}
