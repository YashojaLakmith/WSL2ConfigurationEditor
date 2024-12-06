using Core.Abstractions.Attributes;
using Core.Attributes;

namespace Core.Factories;
public static class AttributeExtractorFactory
{
    public static IAttributeExtracter CreateInstance()
    {
        return new AttributeExtracterImpl();
    }
}
