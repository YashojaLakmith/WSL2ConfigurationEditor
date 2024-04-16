namespace Core.Exceptions;

public class InvalidSettingException : Exception
{
    public InvalidSettingException() : base() { }

    public InvalidSettingException(string message) : base(message) { }
}
