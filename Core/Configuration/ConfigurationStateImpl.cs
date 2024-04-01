using Core.Attributes;
using Core.Enumerations;
using Core.Extensions;
using Core.Parsers;
using Core.SettingEntities;

namespace Core.Configuration;

// This implementation is supposed to be used as a singleton service.
public class ConfigurationStateImpl : IConfigurationState
{
    private readonly Dictionary<string, Dictionary<string, string>> _configDict;
    private readonly IAttributeExtracter _attributeExtractor;
    private readonly IStringOperations _strOps;

    public ConfigurationStateImpl(IAttributeExtracter attributeExtracter, IStringOperations strOps)
    {
        _configDict = [];
        _attributeExtractor = attributeExtracter;
        _strOps = strOps;
    }

    public void ClearState()
    {
        lock (_configDict)
        {
            _configDict.Clear();
        }
    }

    public void LoadConfiguration(IEnumerable<string> configLines)
    {
        lock (_configDict)
        {
            var temp = new Dictionary<string, Dictionary<string, string>>(_configDict);
            Dictionary<string, string> subDict = [];
            _configDict.Clear();
            _configDict.Add(SectionType.Common.ToLowerCaseString(), subDict);

            try
            {
                foreach (var item in configLines)
                {
                    if (!_strOps.IsWslConfigTag(item))
                    {
                        ProcessLine(item, ref subDict);
                    }
                }
            }
            catch (Exception)
            {
                _configDict.Clear();
                foreach (var item in temp)
                {
                    _configDict.TryAdd(item.Key, item.Value);
                }

                throw;
            }
        }
    }

    public List<string> ParseAsConfigLines()
    {
        List<string> lines = [];

        lock (_configDict)
        {
            foreach (var subDict in _configDict)
            {
                if (!subDict.Key.Equals(SectionType.Common.ToLowerCaseString()))
                {
                    lines.Add(@$"[{subDict.Key}]");
                }

                foreach (var kvp in subDict.Value)
                {
                    lines.Add($@"{kvp.Key}={kvp.Value}");
                }
            }
        }

        return lines;
    }

    public void EraseSetting<TSetting>() where TSetting : class, ISettingEntity
    {
        var settingSttr = _attributeExtractor.TryExtractAttribute<SettingAttribute, TSetting>();

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingSttr.Section, out var subDict))
            {
                return;
            }

            subDict.Remove(settingSttr.SettingKey);
        }
    }

    public TSetting GetSetting<TSetting>() where TSetting : class, ISettingEntity, new()
    {
        var conf = new TSetting();
        var settingAttr = _attributeExtractor.TryExtractAttribute<SettingAttribute, TSetting>();
        string? value;

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingAttr.Section, out var subDict))
            {
                throw new Exception();
            }
            if (!subDict.TryGetValue(settingAttr.SettingKey, out value))
            {
                throw new Exception();
            }
        }

        conf.SetValue(value);
        return conf;
    }

    public void UpdateSetting<TSetting>(TSetting setting) where TSetting : class, ISettingEntity
    {
        var settingAttr = _attributeExtractor.TryExtractAttribute<SettingAttribute, TSetting>();
        var settingValue = setting.ParseValueAsString();

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingAttr.Section, out var subDict))
            {
                subDict = [];
                _configDict.TryAdd(settingAttr.Section, subDict);
            }

            if (!subDict.ContainsKey(settingAttr.SettingKey))
            {
                subDict.TryAdd(settingAttr.SettingKey, settingValue);
                return;
            }

            subDict[settingAttr.SettingKey] = settingValue;
        }
    }

    private void ProcessLine(string line, ref Dictionary<string, string> subDict)
    {
        if (_strOps.IsValidKeyValueLine(line))
        {
            var kvp = _strOps.CreateKeyValuePair(line);
            subDict.TryAdd(kvp.Key, kvp.Value);
            return;
        }

        if (_strOps.IsValidSectionTag(line))
        {
            subDict = [];
            var sectionName = _strOps.ExtractSectionName(line);
            _configDict.TryAdd($@"{sectionName}", subDict);
        }
    }
}
