namespace Core.SettingEntities
{
    public class DNSTunneling : BooleanSettingEntity, ISettingEntity
    {
        public DNSTunneling() { }

        public DNSTunneling(bool isEnabled)
        {
            Value = isEnabled;
        }
    }
}
