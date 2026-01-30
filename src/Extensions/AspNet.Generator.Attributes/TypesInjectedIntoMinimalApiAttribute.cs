namespace GhostServiceBuster.AspNet.Generator;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class TypesInjectedIntoMinimalApiAttribute(params Type[] types) : Attribute
{
    internal Type[] Types { get; } = types;
}