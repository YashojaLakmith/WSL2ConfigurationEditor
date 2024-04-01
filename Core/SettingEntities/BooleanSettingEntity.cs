namespace Core.SettingEntities;

public abstract class BooleanSettingEntity
{
    public bool Value { get; protected set; }

    public string ParseValueAsString()
    {
        return Value.ToString().ToLower();
    }

    public void SetValue(string valueAsString)
    {
        Value = TryParseStringAsBool(valueAsString);
    }

    private static bool TryParseStringAsBool(ReadOnlySpan<char> valueAsString)
    {
        if(bool.TryParse(valueAsString, out var result))
        {
            return result;
        }

        throw new InvalidOperationException();
    }
}
