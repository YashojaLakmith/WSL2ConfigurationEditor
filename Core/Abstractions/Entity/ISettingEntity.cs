namespace Core.Abstractions.Entity;

public interface ISettingEntity
{
    public bool IsDefault { get; }

    /// <summary>
    /// Sets the value of the setting to the given value represented by string. Throws if invalid format.
    /// </summary>
    void SetValue(string valueAsString);

    /// <summary>
    /// Parses the current value of the setting to its string representation along with units when applicable.
    /// </summary>
    string ParseValueAsString();

    /// <summary>
    /// Sets the setting to its default.
    /// </summary>
    void SetDefaultValue();
}
