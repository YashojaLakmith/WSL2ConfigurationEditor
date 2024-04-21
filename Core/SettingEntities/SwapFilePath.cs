using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"swapFile", SectionType.Common, @"An absolute Windows path to the swap virtual hard disk.")]
    [SupportedWindowsVersion(10)]
    public class SwapFilePath : BaseDefaultableEntity, ISettingEntity
    {
        public string PathToSwapFile { get; private set; }

        public SwapFilePath() : base(true)
        {
            PathToSwapFile = string.Empty;
        }

        public SwapFilePath(string path) : base(false)
        {
            ValidateWin32Path(path);
            PathToSwapFile = path;
        }

        public string ParseValueAsString()
        {
            return PathToSwapFile.Replace(@"\", @"\\");
        }

        public void SetValue(string valueAsString)
        {
            valueAsString = valueAsString.Replace(@"\\", @"\");
            ValidateWin32Path(valueAsString);
            PathToSwapFile = valueAsString;
            IsDefault &= false;
        }

        private static void ValidateWin32Path(string path)
        {
            string file;
            string? dir;

            if(string.IsNullOrWhiteSpace(path) || path.Equals(string.Empty))
            {
                throw new FormatException(@"Path should not be empty string or a string containing only whitespace characters.");
            }

            try
            {
                file = Path.GetFileName(path);
                dir = Path.GetDirectoryName(path);
            }
            catch (ArgumentException)
            {
                throw new FormatException(@"Path contains one or more invalid characters. Given path should be a valid Win32 path.");
            }
            catch (PathTooLongException)
            {
                throw new FormatException(@"Path contains characters more than the system defined limit.");
            }

            if (!Path.IsPathRooted(path))
            {
                throw new FormatException(@"Path should be an absolute Win32 path");

            }

            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            if (invalidFileNameChars.Intersect(file).Any() || path.Contains('/'))
            {
                throw new FormatException(@"File name contains invalid characters. File name should be a valid Win32 name.");
            }

            var ext = Path.GetExtension(path);
            if (ext is null || !ext.Equals(@".vhdx", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(@"Swap file must be a .vhdx file.");
            }
        }
    }
}
