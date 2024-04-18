using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities
{
    [Setting(@"networkingMode", SectionType.Common, @"Switches between Mirrored and NAT networking modes.")]
    [SupportedWindowsVersion(10, 0, 19045, 1766)]
    public class NetworkingMode : BaseDefaultableEntity, ISettingEntity
    {
        public NetworkMode NetworkMode { get; private set; }

        public NetworkingMode() : base(true)
        {
            NetworkMode = NetworkMode.NAT;
        }

        public NetworkingMode(NetworkMode mode) : base(false)
        {
            NetworkMode = mode;
        }

        public string ParseValueAsString()
        {
            return NetworkMode.ToString().ToLower();
        }

        public void SetValue(string valueAsString)
        {
            switch (valueAsString.ToLower())
            {
                case @"nat":
                    NetworkMode = NetworkMode.NAT;
                    IsDefault &= false;
                    return;

                case @"mirrored":
                    NetworkMode = NetworkMode.Mirrored;
                    IsDefault &= false;
                    return;

                default:
                    NetworkMode = NetworkMode.NAT;
                    IsDefault &= false;
                    return;
            }
        }
    }
}
