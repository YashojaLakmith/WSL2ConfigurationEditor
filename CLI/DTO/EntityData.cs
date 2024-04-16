namespace CLI.DTO;
public record EntityData(
    string SectionName,
    string CommonName,
    string SettingId,
    string SettingValue,
    bool IsSupported
    );
