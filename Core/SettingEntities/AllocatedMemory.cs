using System.Runtime.InteropServices;

using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;
using Core.Exceptions;

namespace Core.SettingEntities
{
    [Setting(@"memory", SectionType.Common, @"Allocated Memory")]
    [SupportedWindowsVersion(10)]
    public class AllocatedMemory : BaseDefaultableEntity, ISettingEntity
    {
        public ulong MemeoryInMegabytes { get; private set; }

        public AllocatedMemory(): base(true) { }

        public AllocatedMemory(uint value): base(false)
        {
            ValidateMemory(value);
            MemeoryInMegabytes = value;
        }

        public void SetValue(string valueAsString)
        {
            var mem = TryParseValue(valueAsString);
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
            if(ulong.TryParse(valueStr, out var result))
            {
                return result;
            }

            throw new FormatException(@"Given value is not a valid value.");
        }

        private static void ValidateMemory(ulong mb)
        {
            var totalMem = GetPhysicalMemory();

            if (mb < 1)
            {
                throw new ArgumentOutOfRangeException(@"Allocated memory cannot be less than 1MB.");
            }

            if(mb > totalMem)
            {
                throw new ArgumentOutOfRangeException(@"Allocated memory cannot be higher than total physical memory.");
            }
        }

        private static ulong GetPhysicalMemory()
        {
            var memStat = new MEMORYSTATUSEX();
            if (!GlobalMemoryStatusEx(memStat))
            {
                throw new PlatformInvokeException(@"Unable to get the installed physical memory.");
            }

            return ConvertToMegaBytes(memStat.ullTotalPhys);
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
            var byteValue = memoryInBytes / Math.Pow(1024, 2);
            return (ulong)Math.Round(byteValue);
        }
    }
}
