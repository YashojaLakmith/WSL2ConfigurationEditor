using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using Core.Exceptions;

namespace Core.SystemInterfaces;

public class SystemInterfacesImpl : ISystemInterfaces
{
    public OperatingSystem GetOperatingSystemInfo()
    {
        return Environment.OSVersion;
    }

    public int GetProcessorCount()
    {
        return Environment.ProcessorCount;
    }

    public int GetTotalPhysicalMemoryInMegaBytes()
    {
        var memoryInBytes = GetPhysicalMemory();
        return ConvertToMegaBytes(memoryInBytes);
    }

    public void RestartWslProcess()
    {
        StopWsl2();
    }

    private static ulong GetPhysicalMemory()
    {
        var memStat = new MEMORYSTATUSEX();
        if (!GlobalMemoryStatusEx(memStat))
        {
            throw new PlatformInvokeException(@"Unable to get the installed physical memory.");
        }

        return memStat.ullTotalPhys;
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

    private static int ConvertToMegaBytes(ulong memoryInBytes)
    {
        var byteValue = memoryInBytes / Math.Pow(1024, 2);
        return (int)Math.Round(byteValue);
    }

    private static void StopWsl2()
    {
        var startInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = true,
            FileName = @"cmd.exe",
            Arguments = @"/C wsl --shutdown"
        };

        try
        {
            using var process = new Process();
            process.StartInfo = startInfo;
            if (process.Start())
            {
                process.WaitForExit();
                return;
            }

            throw new SystemException(@"Could not start the process.");
        }
        catch (Win32Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}
