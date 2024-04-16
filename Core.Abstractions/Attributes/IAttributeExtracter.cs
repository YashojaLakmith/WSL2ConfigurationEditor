namespace Core.Abstractions.Attributes;

public interface IAttributeExtracter
{
    /// <exception cref="AttributeNotFoundException"></exception>
    TAttribute TryExtractAttribute<TAttribute, TTarget>() where TAttribute : Attribute;

    /// <exception cref="AttributeNotFoundException"></exception>
    TAttribute TryExtractAttribute<TAttribute>(object target) where TAttribute : Attribute;
}
