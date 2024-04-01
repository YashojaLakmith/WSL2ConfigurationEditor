namespace Core.Parsers;

public interface IStringOperations
{
    KeyValuePair<string, string> CreateKeyValuePair(ReadOnlySpan<char> line);
    string ExtractSectionName(ReadOnlySpan<char> line);
    bool IsWslConfigTag(string line);
    bool IsValidKeyValueLine(string line);
    bool IsValidSectionTag(string line);
}
