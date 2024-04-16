using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class SparseVHD : BooleanSettingEntity, ISettingEntity
    {
        public SparseVHD() : base(true) { }

        public SparseVHD(bool isAvctive) : base(false)
        {
            Value = isAvctive;
        }
    }
}
