using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"dnsTunneling", SectionType.Common, @"Changes how DNS requests are proxied from WSL to Windows.")]
[SupportedWindowsVersion(10, 0, 22621, 521)]
public class DNSTunneling : BooleanSettingEntity, ISettingEntity
{
    public DNSTunneling() : base(true) { }

    public DNSTunneling(bool isEnabled) : base(false)
    {
        Value = isEnabled;
    }
}
