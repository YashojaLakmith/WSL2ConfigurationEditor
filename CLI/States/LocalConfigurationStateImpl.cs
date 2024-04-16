using CLI.DTO;
using CLI.Extensions;

using Core.Abstractions.Attributes;
using Core.Abstractions.Configuration;
using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Exceptions;
using Core.SettingEntities;

namespace CLI.States;

public class LocalConfigurationStateImpl : ILocalConfigurationState
{
    private readonly List<ISettingEntity> _settings;
    private readonly IConfigurationState _state;
    private readonly IAttributeExtracter _extractor;

    public LocalConfigurationStateImpl(IConfigurationState state, IAttributeExtracter extracter)
    {
        _settings = [];
        _state = state;
        _extractor = extracter;
        GetAllSettingsFromState();
    }

    public List<EntityData> GetAllSettingData()
    {
        return _settings.Select(s => s.AsEntityData())
            .ToList();
    }

    public ISettingEntity GetSettingByKey(string key)
    {
        foreach (var item in _settings)
        {
            if(IsMatchingEntity(item, key))
            {
                return item;
            }
        }

        throw new InvalidOperationException(@"Could not find a setting with the given key");
    }

    public void CommitChanges()
    {
        foreach (var setting in _settings)
        {
            try
            {
                _state.UpdateSetting(setting);
            }
            catch (InvalidSettingException)
            {
                continue;
            }
        }
    }

    public void ResetChanges()
    {
        _settings.Clear();
        GetAllSettingsFromState();
    }

    private void GetAllSettingsFromState()
    {
        TryGetSetting<KernelPath>();
        TryGetSetting<AllocatedMemory>();
        TryGetSetting<AllocatedProcessors>();
        TryGetSetting<LocalhostForwarding>();
        TryGetSetting<KernelCommandLine>();
        TryGetSetting<SafeMode>();
        TryGetSetting<SwapMemory>();
        TryGetSetting<SwapFilePath>();
        TryGetSetting<PageReporting>();
        TryGetSetting<GUIApplications>();
        TryGetSetting<DebugConsole>();
        TryGetSetting<NestedVirtualization>();
        TryGetSetting<VMIdleTimeout>();
        TryGetSetting<DNSProxy>();
        TryGetSetting<NetworkingMode>();
        TryGetSetting<Firewall>();
        TryGetSetting<DNSTunneling>();
        TryGetSetting<AutoProxy>();
        TryGetSetting<AutoMemoryReclaim>();
        TryGetSetting<SparseVHD>();
        TryGetSetting<UseWindowsDNSCache>();
        TryGetSetting<BestEffortDNSParsing>();
        TryGetSetting<InitialAutoProxyTimeout>();
        TryGetSetting<IgnoredPorts>();
        TryGetSetting<HostAddressLoopBack>();
    }

    private void TryGetSetting<T>() where T : class, ISettingEntity, new()
    {
        try
        {
            var setting = _state.GetSetting<T>();
            _settings.Add(setting);
        }
        catch (SettingNotFoundException)
        {
            _settings.Add(new T());
            return;
        }
        catch (InvalidSettingException)
        {
            _settings.Add(new T());
        }
    }

    private bool IsMatchingEntity(ISettingEntity entity, string key)
    {
        var attr = _extractor.TryExtractAttribute<SettingAttribute>(entity);
        return attr.SettingKey.Equals(key, StringComparison.OrdinalIgnoreCase);
    }
}
