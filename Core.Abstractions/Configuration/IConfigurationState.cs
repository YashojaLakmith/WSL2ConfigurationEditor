using Core.Abstractions.Entity;

namespace Core.Abstractions.Configuration;

public interface IConfigurationState
{
    void ClearState();
    void LoadConfiguration(IEnumerable<string> configLines);

    /// <exception cref="SettingNotFoundException"></exception>"
    /// <exception cref="InvalidSettingException"></exception>
    TSetting GetSetting<TSetting>() where TSetting : class, ISettingEntity, new();

    /// <exception cref="InvalidSettingAttribute"></exception>"
    void EraseSetting<TSetting>() where TSetting : class, ISettingEntity;

    /// <exception cref="InvalidSettingAttribute"></exception>"
    void UpdateSetting(ISettingEntity setting);
    List<string> ParseAsConfigLines();
}
