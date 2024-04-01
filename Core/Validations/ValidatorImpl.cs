using Core.SystemInterfaces;

namespace WSL2ConfigurationEditor.Core.Validations;

public class ValidatorImpl(ISystemInterfaces sysInterface) : IValidator
{
    private readonly ISystemInterfaces _sysInterface = sysInterface;

    public bool IsValidCpuCount(int cpuCount)
    {
        var sysCpuCout = _sysInterface.GetProcessorCount();
        return cpuCount > 0
            && sysCpuCout >= cpuCount;
    }

    public bool IsValidMemoryValue(int memoryInMB)
    {
        var totalPhysicalMem = _sysInterface.GetTotalPhysicalMemoryInMegaBytes();
        return memoryInMB > 0
            && totalPhysicalMem >= memoryInMB;
    }

    public void ThrowIfCollectionIsEmpty<T>(IEnumerable<T> collection)
    {
        if (collection.Any())
        {
            return;
        }

        var type = typeof(T).Name;
        throw new ArgumentException(@$"The {type} collection is empty");
    }
}