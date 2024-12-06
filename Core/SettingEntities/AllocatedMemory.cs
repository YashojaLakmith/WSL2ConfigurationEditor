using System.Runtime.InteropServices;

using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;
using Core.Exceptions;

namespace Core.SettingEntities;

[Setting(@"memory", SectionType.Common, @"Allocated Memory in MegaBytes")]
[SupportedWindowsVersion(10)]
public class AllocatedMemory : BaseDefaultableEntity, ISettingEntity
{
    public ulong MemeoryInMegabytes { get; private set; }

    public AllocatedMemory() : base(true) { }

    public AllocatedMemory(uint value) : base(false)
    {
        ValidateMemory(value);
        MemeoryInMegabytes = value;
    }

    public void SetValue(string valueAsString)
    {
        ulong mem = TryParseValue(valueAsString);
        ValidateMemory(mem);
        MemeoryInMegabytes = mem;
        IsDefault &= false;
    }

    public string ParseValueAsString()
    {
        return $@"{MemeoryInMegabytes}MB";
    }

    private static ulong TryParseValue(ReadOnlySpan<char> valueStr)
    {
        return ulong.TryParse(valueStr, out ulong result)
            ? result
            : TryParseValueWithUnit(valueStr);

        throw new FormatException(@"Given value is not a valid value.");
    }

    private static void ValidateMemory(ulong mb)
    {
        ulong totalMem = GetPhysicalMemory();

        if (mb < 1)
        {
            throw new ArgumentOutOfRangeException(@"Allocated memory cannot be less than 1MB.");
        }

        if (mb > totalMem)
        {
            throw new ArgumentOutOfRangeException(@"Allocated memory cannot be higher than total physical memory.");
        }
    }

    private static ulong GetPhysicalMemory()
    {
        MEMORYSTATUSEX memStat = new();
        return !GlobalMemoryStatusEx(memStat)
            ? throw new PlatformInvokeException(@"Unable to get the installed physical memory.")
            : ConvertToMegaBytes(memStat.ullTotalPhys);
    }

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport(@"kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, CallingConvention = CallingConvention.StdCall)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
        }
    }

    private static ulong ConvertToMegaBytes(ulong memoryInBytes)
    {
        double byteValue = memoryInBytes / Math.Pow(1024, 2);
        return (ulong)Math.Round(byteValue);
    }

    private static ulong TryParseValueWithUnit(ReadOnlySpan<char> str)
    {
        int length = str.Length;
        if (length < 3)
        {
            throw new FormatException(@"Given value is not a valid value.");
        }

        ReadOnlySpan<char> value = str[..(length - 2)];
        ReadOnlySpan<char> unit = str[(length - 2)..];

        return !ulong.TryParse(value, out ulong result)
            ? throw new FormatException(@"Given value is not a valid value.")
            : TryParseUnit(unit) * result;
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
