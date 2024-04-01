namespace Core.SettingEntities
{
    public class SparseVHD : BooleanSettingEntity, ISettingEntity
    {
        public SparseVHD() { }

        public SparseVHD(bool isAvctive)
        {
            Value = isAvctive;
        }
    }
}
