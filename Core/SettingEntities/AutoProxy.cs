
namespace Core.SettingEntities
{
    public class AutoProxy : ISettingEntity
    {
        public bool IsEnabled { get; private set; }

        public AutoProxy() { }

        public AutoProxy(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public void SetValue(string valueAsString)
        {
            IsEnabled = TryParseStringToBool(valueAsString);
        }

        public string ParseValueAsString()
        {
            return IsEnabled.ToString().ToLower();
        }

        private static bool TryParseStringToBool(ReadOnlySpan<char> valueAsString)
        {
            if(bool.TryParse(valueAsString, out var result))
            {
                return result;
            }

            throw new InvalidOperationException();
        }
    }
}
