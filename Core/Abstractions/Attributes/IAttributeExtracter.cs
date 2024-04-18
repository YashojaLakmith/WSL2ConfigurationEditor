namespace Core.Abstractions.Attributes;

public interface IAttributeExtracter
{
    /// <summary>
    /// Gets the given custom attribute which the target type is annotated with. Throws if not found. 
    /// </summary>
    /// <typeparam name="TAttribute">Attribute to extract.</typeparam>
    /// <typeparam name="TTarget">The type which is the attribute has been used on.</typeparam>
    /// <returns>The extracted attribute instance.</returns>
    TAttribute TryExtractAttribute<TAttribute, TTarget>() where TAttribute : Attribute;

    /// <summary>
    /// Gets the given custom attribute which the type of given object is annotated with. Throws if not found. 
    /// </summary>
    /// <typeparam name="TAttribute">Attribute to extract.</typeparam>
    /// <returns>The extracted attribute instance.</returns>
    TAttribute TryExtractAttribute<TAttribute>(object target) where TAttribute : Attribute;
}
