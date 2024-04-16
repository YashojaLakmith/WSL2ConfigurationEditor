using CLI.DTO;

using Core.Abstractions.Entity;

namespace CLI.States;
public interface ILocalConfigurationState
{
    void ResetChanges();
    void CommitChanges();
    List<EntityData> GetAllSettingData();
    ISettingEntity GetSettingByKey(string key);
}