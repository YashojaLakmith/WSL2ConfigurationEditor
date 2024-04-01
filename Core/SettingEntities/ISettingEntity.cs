namespace Core.SettingEntities;

public interface ISettingEntity
{
    void SetValue(string valueAsString);
    string ParseValueAsString();
}
