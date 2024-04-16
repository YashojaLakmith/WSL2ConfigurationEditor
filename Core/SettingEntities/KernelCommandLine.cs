using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
    public class KernelCommandLine : BaseDefaultableEntity, ISettingEntity
    {
        public string CommandLineArgs { get; private set; }

        public KernelCommandLine() : base(true)
        {
            CommandLineArgs = string.Empty;
        }

        public KernelCommandLine(string commandLineArgs) : base(false)
        {
            CommandLineArgs = commandLineArgs;
        }

        public string ParseValueAsString()
        {
            return CommandLineArgs;
        }

        public void SetValue(string valueAsString)
        {
            CommandLineArgs = valueAsString;
            IsDefault &= false;
        }
    }
}
