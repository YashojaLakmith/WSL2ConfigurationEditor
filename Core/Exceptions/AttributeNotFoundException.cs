namespace Core.Exceptions;

public class AttributeNotFoundException : Exception
{
    public AttributeNotFoundException() : base() { }

    public AttributeNotFoundException(string message) : base(message) { }
}
