using System.Reflection;

using Core.Exceptions;

namespace Core.Attributes;

public class AttributeExtracterImpl : IAttributeExtracter
{
    public TAttribute TryExtractAttribute<TAttribute, TTarget>() where TAttribute : Attribute
    {
        var typeInfo = typeof(TTarget).GetTypeInfo();
        var customAttr = typeInfo.GetCustomAttribute<TAttribute>();

        return customAttr is null ? throw new AttributeNotFoundException(@$"Type {typeof(TTarget).Name} has not been decorated by {typeof(TAttribute).Name}.") : customAttr;
    }
}