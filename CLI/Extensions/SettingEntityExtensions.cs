﻿using CLI.DTO;
using CLI.ServiceFactories;
using Core.Abstractions.Attributes;
using Core.Abstractions.Entity;
using Core.Attributes;

namespace CLI.Extensions;

public static class SettingEntityExtensions
{
    public static EntityData AsEntityData(this ISettingEntity entity)
    {
        try
        {
            var extractor = DefaultServiceFactory.ResolveService<IAttributeExtracter>();
            var settingAttr = extractor.TryExtractAttribute<SettingAttribute>(entity);
            var osAttr = extractor.TryExtractAttribute<SupportedWindowsVersionAttribute>(entity);

            var value = entity.IsDefault ? @"#DEFAULT" : entity.ParseValueAsString().Replace(@"\\", @"\");
            var section = string.Concat(settingAttr.Section[0]
                .ToString()
                .ToUpper(), settingAttr.Section.AsSpan(1));

            return new($@"{section} Setting", settingAttr.CommonName, settingAttr.SettingKey, value, osAttr.IsSupportedOnThisPlatform());
        }
        catch (AggregateException)
        {
            throw new InvalidDataException(@"An error occured while parsing the settings to visualize.");
        }
    }
}
