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
    private readonly Dictionary<string, Dictionary<string, string>> _configDict;
    private readonly IAttributeExtracter _attributeExtractor;

    public ConfigurationStateImpl(IAttributeExtracter attributeExtracter)
    {
        _configDict = [];
        _attributeExtractor = attributeExtracter;
    }

    public void ClearState()
    {
        lock (_configDict)
        {
            _configDict.Clear();
        }
    }

    public void LoadConfiguration(IEnumerable<string> configLines)  // Completed
    {
        lock (_configDict)
        {
            Dictionary<string, string> subDict = [];
            _configDict.Clear();
            _configDict.Add(SectionAsLowecaseString(SectionType.Common), subDict);

            foreach (var item in configLines)
            {
                if (!IsWslConfigTag(item))
                {
                    ProcessLine(item, ref subDict);
                }
            }
        }
    }

    public List<string> ParseAsConfigLines()    // Completed
    {
        List<string> lines = [];

        lock (_configDict)
        {
            foreach (var subDict in _configDict)
            {
                if (!subDict.Key.Equals(SectionAsLowecaseString(SectionType.Common)))
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

    public void EraseSetting<TSetting>() where TSetting : class, ISettingEntity     // Completed
    {
        var settingSttr = TryGetSettingAttribute<TSetting>();

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingSttr.Section, out var subDict))
            {
                return;
            }

            subDict.Remove(settingSttr.SettingKey);
        }
    }

    public TSetting GetSetting<TSetting>()  where TSetting : class, ISettingEntity, new()    // Completed
    {
        var conf = new TSetting();
        var settingAttr = TryGetSettingAttribute<TSetting>();

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingAttr.Section, out var subDict))
            {
                throw new SettingNotFoundException(settingAttr);
            }
            if (!subDict.TryGetValue(settingAttr.SettingKey, out var value))
            {
                throw new SettingNotFoundException(settingAttr);
            }

            conf.SetValue(value);
            return conf;
        }
    }   

    public void UpdateSetting(ISettingEntity setting)   // Completed
    {
        var settingAttr = TryGetSettingAttribute(setting);
        var settingValue = setting.ParseValueAsString();

        lock (_configDict)
        {
            if (!_configDict.TryGetValue(settingAttr.Section, out var subDict))
            {
                subDict = [];
                _configDict.TryAdd(settingAttr.Section, subDict);
            }

            if (!subDict.ContainsKey(settingAttr.SettingKey) && !setting.IsDefault)
            {
                subDict.TryAdd(settingAttr.SettingKey, settingValue);
                return;
            }

            if (setting.IsDefault)
            {
                subDict.Remove(settingAttr.SettingKey);
                return;
            }

            subDict[settingAttr.SettingKey] = settingValue;
        }
    }

    private void ProcessLine(string line, ref Dictionary<string, string> subDict)
    {
        if (IsValidKeyValueLine(line))
        {
            try
            {
                var kvp = CreateKeyValuePair(line);
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
            var sectionName = ExtractSectionName(line);
            _configDict.TryAdd($@"{sectionName}", subDict);
        }
    }

    private KeyValuePair<string, string> CreateKeyValuePair(ReadOnlySpan<char> line)
    {
        var idx = line.IndexOf('=');
        if (idx < 1)
        {
            throw new InvalidOperationException(@"Line cannot be parsed as a key-value pair.");
        }

        return SplitToKeyValuePair(line, idx);
    }

    private static string ExtractSectionName(ReadOnlySpan<char> line)
    {
        var length = line.Length;
        var lastIndex = length - 1;
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
        var key = line[..index].ToString();
        var value = line[(index + 1)..].ToString();

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
