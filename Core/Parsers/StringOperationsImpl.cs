namespace Core.Parsers;

public class StringOperationsImpl : IStringOperations
{
    public KeyValuePair<string, string> CreateKeyValuePair(ReadOnlySpan<char> line)
    {
        var idx = line.IndexOf('=');
        if (idx < 1)
        {
            throw new InvalidOperationException();
        }

        return SplitToKeyValuePair(line, idx);
    }

    public string ExtractSectionName(ReadOnlySpan<char> line)
    {
        var length = line.Length;
        var lastIndex = length - 1;
        return line[1..lastIndex].ToString();
    }

    public bool IsValidKeyValueLine(string line)
    {
        return !(line.StartsWith('[') && line.EndsWith(']'))
            && !line.StartsWith('#')
            && !string.IsNullOrEmpty(line);
    }

    public bool IsValidSectionTag(string line)
    {
        return !string.IsNullOrEmpty(line)
            && line.StartsWith('[')
            && line.EndsWith(']');
    }

    public bool IsWslConfigTag(string line)
    {
        return line.Equals(@"[wslconfig]");
    }

    private static KeyValuePair<string, string> SplitToKeyValuePair(ReadOnlySpan<char> line, int index)
    {
        var key = line[..index].ToString();
        var value = line[(index + 1)..].ToString();

        return KeyValuePair.Create(key, value);
    }
}
