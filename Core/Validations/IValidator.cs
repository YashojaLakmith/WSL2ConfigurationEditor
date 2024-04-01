namespace WSL2ConfigurationEditor.Core.Validations
{
    public interface IValidator
    {
        void ThrowIfCollectionIsEmpty<T>(IEnumerable<T> collection);
        bool IsValidMemoryValue(int memoryInMB);
        bool IsValidCpuCount(int cpuCount);
    }
}
