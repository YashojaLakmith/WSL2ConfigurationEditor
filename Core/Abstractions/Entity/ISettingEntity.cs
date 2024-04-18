namespace Core.Abstractions.Entity;

public interface ISettingEntity
{
    public bool IsDefault { get; }

    /// <exception cref="FormatException"></exception>
    void SetValue(string valueAsString);
    string ParseValueAsString();
    void SetDefaultValue();
}
