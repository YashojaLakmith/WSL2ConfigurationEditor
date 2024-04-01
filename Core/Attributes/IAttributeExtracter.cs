namespace Core.Attributes;

public interface IAttributeExtracter
{
    TAttribute TryExtractAttribute<TAttribute, TTarget>() where TAttribute : Attribute;
}
