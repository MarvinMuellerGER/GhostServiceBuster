namespace GhostServiceBuster.MS.Generator;

/// <summary>
/// Marks assemblies with types that are resolved directly by a service provider.
/// </summary>
/// <param name="types">The types resolved by the service provider.</param>
[AttributeUsage(AttributeTargets.Assembly)]
public sealed class TypesResolvedByServiceProviderAttribute(params Type[] types) : Attribute
{
    internal Type[] Types { get; } = types;
}
