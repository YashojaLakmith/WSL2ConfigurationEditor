using System.Text;

using Core.Abstractions.Entity;

namespace Core.SettingEntities
{
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
            var list = new List<uint>();
            var parts = valueAsString.Split(',');

            foreach (var part in parts)
            {
                if(!uint.TryParse(part, out var result))
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
            var builder = new StringBuilder();
            var count = Ports.Count;

            if(count < 1)
            {
                return string.Empty;
            }
            
            for(var i = 0; i < count; i++)
            {
                builder.Append(Ports[i]);
                if(i != (count - 1))
                {
                    builder.Append(',');
                }
            }

            return builder.ToString();
        }
    }
}
