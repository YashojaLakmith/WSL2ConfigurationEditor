﻿using Core.Enumerations;
using Core.Extensions;

namespace Core.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SettingAttribute : Attribute
{
    public string SettingKey { get; }
    public string Section { get; }
    public string CommonName { get; }

    public SettingAttribute(string settingKey, SectionType sectionName, string commonName = @"")
    {
        SettingKey = settingKey;
        Section = sectionName.ToLowerCaseString();

        if (string.IsNullOrWhiteSpace(commonName) || commonName.Equals(string.Empty))
        {
            CommonName = SettingKey;
        }
        else
        {
            CommonName = commonName;
        }
    }
}
