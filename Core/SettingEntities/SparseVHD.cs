using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"sparseVhd", SectionType.Experimental, @"When set to true, any newly created VHD will be set to sparse automatically.")]
    [SupportedWindowsVersion(10)]
    public class SparseVHD : BooleanSettingEntity, ISettingEntity
    {
        public SparseVHD() : base(true) { }

        public SparseVHD(bool isAvctive) : base(false)
        {
            Value = isAvctive;
        }
    }
}
