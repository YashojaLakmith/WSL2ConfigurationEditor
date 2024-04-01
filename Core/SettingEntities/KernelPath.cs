namespace Core.SettingEntities
{
    public class KernelPath
    {
        private const string KeyName = @"kernel";
        public string KernelPathWin32Format { get; private set; }

        public KeyValuePair<string, string> ParseIntoKeyValuePair()
        {
            if (string.IsNullOrEmpty(KernelPathWin32Format))
            {
                return new KeyValuePair<string, string>();
            }

            return KeyValuePair.Create(KeyName, KernelPathWin32Format);
        }

        public void SetKernelPath(string path)
        {
            KernelPathWin32Format = path;
        }

        public static KernelPath Create(string valueString)
        {
            return new(valueString);
        }

        public static KernelPath Create()
        {
            return new();
        }

        private KernelPath(string kernelPath)
        {
            KernelPathWin32Format = kernelPath;
        }

        private KernelPath()
        {
            KernelPathWin32Format = string.Empty;
        }
    }
}
