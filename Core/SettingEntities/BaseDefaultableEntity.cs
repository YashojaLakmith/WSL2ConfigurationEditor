namespace Core.SettingEntities;

public abstract class BaseDefaultableEntity
{
    public bool IsDefault { get; protected set; }

    public BaseDefaultableEntity(bool isDefault)
    {
        IsDefault = isDefault;
    }

    public void SetDefaultValue()
    {
        IsDefault = true;
    }
}
