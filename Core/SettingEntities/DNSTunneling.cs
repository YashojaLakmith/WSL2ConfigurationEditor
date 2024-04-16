using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class DNSTunneling : BooleanSettingEntity, ISettingEntity
    {
        public DNSTunneling(): base(true) { }

        public DNSTunneling(bool isEnabled): base(false)
        {
            Value = isEnabled;
        }
    }
}
