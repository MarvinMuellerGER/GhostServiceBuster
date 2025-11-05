namespace GhostServiceBuster.MS.Generator;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class TypesResolvedByServiceProviderAttribute(params Type[] types) : Attribute
{
    internal Type[] Types { get; } = types;
}