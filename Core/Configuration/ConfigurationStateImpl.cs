using Core.Abstractions.Attributes;
using Core.Abstractions.Configuration;
using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;
using Core.Exceptions;

namespace Core.Configuration;

// This implementation is supposed to be used as a singleton service.
public class ConfigurationStateImpl : IConfigurationState
{
    private readonly Dictionary<string, Dictionary<string, string>> _configDictionary;
    private readonly IAttributeExtracter _attributeExtractor;

    public ConfigurationStateImpl(IAttributeExtracter attributeExtracter)
    {
        _configDictionary = [];
        _attributeExtractor = attributeExtracter;
    }

    public void ClearState()
    {
        lock (_configDictionary)
        {
            _configDictionary.Clear();
        }
    }

    public void LoadConfiguration(IEnumerable<string> configLines)
    {
        lock (_configDictionary)
        {
            Dictionary<string, string> subConfigDictionary = [];
            _configDictionary.Clear();
            _configDictionary.Add(SectionAsLowecaseString(SectionType.Common), subConfigDictionary);

            foreach (string item in configLines)
            {
                if (!IsWslConfigTag(item))
                {
                    ProcessLine(item, ref subConfigDictionary);
                }
            }
        }
    }

    public List<string> ParseAsConfigLines()
    {
        List<string> configurationLines = [];

        lock (_configDictionary)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> subDict in _configDictionary)
            {
                if (!subDict.Key.Equals(SectionAsLowecaseString(SectionType.Common)))
                {
                    configurationLines.Add(@$"[{subDict.Key}]");
                }

                foreach (KeyValuePair<string, string> kvp in subDict.Value)
                {
                    configurationLines.Add($@"{kvp.Key}={kvp.Value}");
                }
            }
        }

        return configurationLines;
    }

    public void EraseSetting<TSetting>() where TSetting : class, ISettingEntity
    {
        SettingAttribute settingSttr = TryGetSettingAttribute<TSetting>();

        lock (_configDictionary)
        {
            if (!_configDictionary.TryGetValue(settingSttr.Section, out Dictionary<string, string>? subDict))
            {
                return;
            }

            subDict.Remove(settingSttr.SettingKey);
        }
    }

    public TSetting GetSetting<TSetting>() where TSetting : class, ISettingEntity, new()
    {
        TSetting conf = new();
        SettingAttribute settingAttribute = TryGetSettingAttribute<TSetting>();

        lock (_configDictionary)
        {
            if (!_configDictionary.TryGetValue(settingAttribute.Section, out Dictionary<string, string>? subDict))
            {
                throw new SettingNotFoundException(settingAttribute);
            }
            if (!subDict.TryGetValue(settingAttribute.SettingKey, out string? value))
            {
                throw new SettingNotFoundException(settingAttribute);
            }

            conf.SetValue(value);
            return conf;
        }
    }

    public void UpdateSetting(ISettingEntity setting)
    {
        SettingAttribute settingAttribute = TryGetSettingAttribute(setting);
        string settingValue = setting.ParseValueAsString();

        lock (_configDictionary)
        {
            if (!_configDictionary.TryGetValue(settingAttribute.Section, out Dictionary<string, string>? subDict))
            {
                subDict = [];
                _configDictionary.TryAdd(settingAttribute.Section, subDict);
            }

            if (!subDict.ContainsKey(settingAttribute.SettingKey) && !setting.IsDefault)
            {
                subDict.TryAdd(settingAttribute.SettingKey, settingValue);
                return;
            }

            if (setting.IsDefault)
            {
                subDict.Remove(settingAttribute.SettingKey);
                return;
            }

            subDict[settingAttribute.SettingKey] = settingValue;
        }
    }

    private void ProcessLine(string line, ref Dictionary<string, string> subDict)
    {
        if (IsValidKeyValueLine(line))
        {
            try
            {
                KeyValuePair<string, string> kvp = CreateKeyValuePair(line);
                subDict.TryAdd(kvp.Key, kvp.Value);
            }
            catch (InvalidOperationException)
            {
            }

            return;
        }

        if (IsValidSectionTag(line))
        {
            subDict = [];
            string sectionName = ExtractSectionName(line);
            _configDictionary.TryAdd($@"{sectionName}", subDict);
        }
    }

    private static KeyValuePair<string, string> CreateKeyValuePair(ReadOnlySpan<char> line)
    {
        int idx = line.IndexOf('=');
        return idx < 1
            ? throw new InvalidOperationException(@"Line cannot be parsed as a key-value pair.")
            : SplitToKeyValuePair(line, idx);
    }

    private static string ExtractSectionName(ReadOnlySpan<char> line)
    {
        int length = line.Length;
        int lastIndex = length - 1;
        return line[1..lastIndex].ToString();
    }

    private static bool IsValidKeyValueLine(string line)
    {
        return !(line.StartsWith('[') && line.EndsWith(']'))
            && !line.StartsWith('#')
            && !string.IsNullOrWhiteSpace(line)
            && !line.Equals(string.Empty);
    }

    private static bool IsValidSectionTag(string line)
    {
        return !string.IsNullOrWhiteSpace(line)
            && !line.Equals(string.Empty)
            && line.StartsWith('[')
            && line.EndsWith(']');
    }

    private static bool IsWslConfigTag(string line)
    {
        return line.Equals(@"[wslconfig]");
    }

    private static KeyValuePair<string, string> SplitToKeyValuePair(ReadOnlySpan<char> line, int index)
    {
        string key = line[..index].ToString();
        string value = line[(index + 1)..].ToString();

        return KeyValuePair.Create(key, value);
    }

    private static string SectionAsLowecaseString(SectionType type)
    {
        return type.ToString().ToLower();
    }

    private SettingAttribute TryGetSettingAttribute<TSetting>()
    {
        try
        {
            return _attributeExtractor.TryExtractAttribute<SettingAttribute, TSetting>();
        }
        catch (AttributeNotFoundException)
        {
            throw new InvalidSettingException(@"Searched setting is not available.");
        }
    }

    private SettingAttribute TryGetSettingAttribute(ISettingEntity entity)
    {
        try
        {
            return _attributeExtractor.TryExtractAttribute<SettingAttribute>(entity);
        }
        catch (AttributeNotFoundException)
        {
            throw new InvalidSettingException(@"Searched setting is not available.");
        }
    }
}
