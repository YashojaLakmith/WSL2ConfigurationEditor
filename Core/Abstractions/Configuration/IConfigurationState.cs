using Core.Abstractions.Entity;

namespace Core.Abstractions.Configuration;

public interface IConfigurationState
{
    /// <summary>
    /// Clears the current state from the memory.
    /// </summary>
    void ClearState();

    /// <summary>
    /// Process the text lines from .wslconfig file as the in-memory representation of the configuration. Discarding already loaded configuration if any.
    /// </summary>
    void LoadConfiguration(IEnumerable<string> configLines);

    /// <summary>
    /// Gets the setting corresponding to the given type as an strongly typed object. Throws if failed.
    /// </summary>
    TSetting GetSetting<TSetting>() where TSetting : class, ISettingEntity, new();

    /// <summary>
    /// Erases the setting corresponding to the given type from the in-memory state.
    /// </summary>
    void EraseSetting<TSetting>() where TSetting : class, ISettingEntity;

    /// <summary>
    /// Updates the setting corresponding to the implementation type of the given instance.
    /// </summary>
    void UpdateSetting(ISettingEntity setting);

    /// <summary>
    /// Returns the in-memory configuration as the text lines representation of .wslconfig file.
    /// </summary>
    List<string> ParseAsConfigLines();
}
