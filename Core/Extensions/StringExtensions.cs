namespace Core.Extensions;

public static class StringExtensions
{
    public static string ToLowerCaseString(this object obj)
    {
        return obj.ToString().ToLower();
    }
}
