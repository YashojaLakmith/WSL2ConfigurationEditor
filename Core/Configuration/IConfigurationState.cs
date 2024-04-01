using Core.SettingEntities;

namespace Core.Configuration;

public interface IConfigurationState
{
    void ClearState();
    void LoadConfiguration(IEnumerable<string> configLines);
    TSetting GetSetting<TSetting>() where TSetting : class, ISettingEntity, new();
    void EraseSetting<TSetting>() where TSetting : class, ISettingEntity;
    void UpdateSetting<TSetting>(TSetting setting) where TSetting : class, ISettingEntity;
    List<string> ParseAsConfigLines();
}
