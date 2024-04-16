using Core.Attributes;

namespace Core.Exceptions;

public class SettingNotFoundException : Exception
{
    public string SettingKey { get; }
    public string Section {  get; }

    public SettingNotFoundException(SettingAttribute attr) : base($@"Could not find the setting with section: '{attr.Section}' and key: '{attr.SettingKey}'.")
    {
        SettingKey = attr.SettingKey;
        Section = attr.Section;
    }
}
