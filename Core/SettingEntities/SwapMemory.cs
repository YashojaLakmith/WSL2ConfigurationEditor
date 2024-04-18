﻿using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"swap", SectionType.Common, @"How much swap space to add to the WSL 2 VM, 0 for no swap file.")]
    [SupportedWindowsVersion(10)]
    public class SwapMemory : BaseDefaultableEntity, ISettingEntity
    {
        public ulong SwapMemoryInMegaBytes { get; private set; }

        public SwapMemory() : base(true) { }

        public SwapMemory(ulong swapMemoryInMegaBytes) : base(false)
        {
            SwapMemoryInMegaBytes = swapMemoryInMegaBytes;
        }

        public string ParseValueAsString()
        {
            return $@"{SwapMemoryInMegaBytes}MB";
        }

        public void SetValue(string valueAsString)
        {
            SwapMemoryInMegaBytes = TryParseValue(valueAsString);
            IsDefault &= false;
        }

        private static ulong TryParseValue(ReadOnlySpan<char> valueStr)
        {
            if (ulong.TryParse(valueStr, out var result))
            {
                return result;
            }

            return TryParseValueWithUnit(valueStr);

            throw new FormatException(@"Given value is not a valid value.");
        }

        private static ulong TryParseValueWithUnit(ReadOnlySpan<char> str)
        {
            var length = str.Length;
            if (length < 3)
            {
                throw new FormatException(@"Given value is not a valid value.");
            }

            var value = str[..(length - 2)];
            var unit = str[(length - 2)..];

            if (!ulong.TryParse(value, out var result))
            {
                throw new FormatException(@"Given value is not a valid value.");
            }

            return TryParseUnit(unit) * result;
        }

        private static ulong TryParseUnit(ReadOnlySpan<char> str)
        {
            if (str.Equals(@"mb", StringComparison.OrdinalIgnoreCase))
            {
                return 1;
            }
            else if (str.Equals(@"gb", StringComparison.OrdinalIgnoreCase))
            {
                return 1024;
            }

            throw new FormatException(@"Given value is not a valid value.");
        }
    }
}
