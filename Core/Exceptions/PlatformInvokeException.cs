namespace Core.Exceptions;

public class PlatformInvokeException : Exception
{
    public PlatformInvokeException() : base() { }

    public PlatformInvokeException(string message) : base(message) { }
}
