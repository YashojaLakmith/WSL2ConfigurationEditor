using System.Text;

using Core.Abstractions.Entity;
using Core.Attributes;
using Core.Enumerations;

namespace Core.SettingEntities;

[Setting(@"ignoredPorts", SectionType.Experimental, @"Only applicable when Networking Mode is set to Mirrored. Specifies which ports Linux applications can bind to, even if that port is used in Windows. This enables applications to listen on a port for traffic purely within Linux, so those applications are not blocked even when that port is used for other purposes on Windows. Should be formatted in a comma separated list, e.g: 3000,9000,9090")]
[SupportedWindowsVersion(10, 0, 19045, 1766)]
public class IgnoredPorts : BaseDefaultableEntity, ISettingEntity
{
    public List<uint> Ports { get; private set; }

    public IgnoredPorts() : base(true)
    {
        Ports = [];
    }

    public IgnoredPorts(IEnumerable<uint> ports) : base(false)
    {
        Ports = new(ports.Distinct());
    }

    public void SetValue(string valueAsString)
    {
        List<uint> list = [];
        string[] parts = valueAsString.Split(',');

        foreach (string part in parts)
        {
            if (!uint.TryParse(part, out uint result))
            {
                throw new FormatException(@"The values should be valid port numbers.");
            }

            list.Add(result);
        }

        Ports.Clear();
        Ports.AddRange(list.Distinct());
        IsDefault &= false;
    }

    public string ParseValueAsString()
    {
        StringBuilder builder = new();
        int count = Ports.Count;

        if (count < 1)
        {
            return string.Empty;
        }

        for (int i = 0; i < count; i++)
        {
            builder.Append(Ports[i]);
            if (i != (count - 1))
            {
                builder.Append(',');
            }
        }

        return builder.ToString();
    }
}
