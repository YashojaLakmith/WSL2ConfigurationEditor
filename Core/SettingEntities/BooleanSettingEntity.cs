namespace Core.SettingEntities;

public abstract class BooleanSettingEntity : BaseDefaultableEntity
{
    public bool Value { get; protected set; }

    public BooleanSettingEntity(bool isDefault): base(isDefault) { }

    public string ParseValueAsString()
    {
        return Value.ToString().ToLower();
    }

    public void SetValue(string valueAsString)
    {
        Value = TryParseStringAsBool(valueAsString);
        IsDefault &= false;
    }

    private static bool TryParseStringAsBool(string valueAsString)
    {
        if(bool.TryParse(valueAsString.ToLower(), out var result))
        {
            return result;
        }

        throw new FormatException(@"The value should either be 'true' or 'false'.");
    }
}
