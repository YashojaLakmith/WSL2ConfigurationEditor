using CLI.DTO;

using Core.Abstractions.Entity;

namespace CLI.Abstractions.States;

/// <summary>
/// Defines methods for manipulating the frontend specific in-memory configuration.
/// </summary>
public interface ILocalConfigurationState
{
    /// <summary>
    /// Reverts all non-commited changes.
    /// </summary>
    void ResetChanges();

    /// <summary>
    /// Updates the main in-memory state with the local configuration.
    /// </summary>
    void CommitChanges();

    /// <summary>
    /// Gets key details about settings as a collection of DTOs.
    /// </summary>
    List<EntityData> GetAllSettingData();

    /// <summary>
    /// Gets the strongly typed object representation of a setting with given key. Throws if not found a setting corresponds to the key.
    /// </summary>
    ISettingEntity GetSettingByKey(string key);
}