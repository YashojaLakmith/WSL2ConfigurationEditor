using System.Reflection;

using Core.Abstractions.Attributes;
using Core.Exceptions;

namespace Core.Attributes;

public class AttributeExtracterImpl : IAttributeExtracter
{
    public TAttribute TryExtractAttribute<TAttribute, TTarget>() where TAttribute : Attribute
    {
        TypeInfo typeInfo = typeof(TTarget).GetTypeInfo();
        TAttribute? customAttr = typeInfo.GetCustomAttribute<TAttribute>();

        return customAttr is null
            ? throw new AttributeNotFoundException(
                @$"Type {typeof(TTarget).Name} has not been decorated by {typeof(TAttribute).Name}.")
            : customAttr;
    }

    public TAttribute TryExtractAttribute<TAttribute>(object target) where TAttribute : Attribute
    {
        TypeInfo typeinfo = target.GetType().GetTypeInfo();
        TAttribute? customAttr = typeinfo.GetCustomAttribute<TAttribute>();

        return customAttr is null
            ? throw new AttributeNotFoundException(
                @$"Type {target.GetType().Name} has not been decorated by {typeof(TAttribute).Name}.")
            : customAttr;
    }
}