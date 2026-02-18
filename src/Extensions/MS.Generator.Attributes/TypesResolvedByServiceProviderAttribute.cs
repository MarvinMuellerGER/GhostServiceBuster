namespace GhostServiceBuster.MS.Generator;

[AttributeUsage(AttributeTargets.Assembly)]
/// <summary>
/// Marks assemblies with types that are resolved directly by a service provider.
/// </summary>
/// <param name="types">The types resolved by the service provider.</param>
public sealed class TypesResolvedByServiceProviderAttribute(params Type[] types) : Attribute
{
    internal Type[] Types { get; } = types;
}
