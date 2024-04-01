namespace Core.SystemInterfaces;

public interface ISystemInterfaces
{
    int GetProcessorCount();
    int GetTotalPhysicalMemoryInMegaBytes();
    OperatingSystem GetOperatingSystemInfo();
    void RestartWslProcess();
}