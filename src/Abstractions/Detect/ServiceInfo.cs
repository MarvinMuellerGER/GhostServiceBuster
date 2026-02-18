namespace GhostServiceBuster.Detect;

/// <summary>
/// Describes a service and its implementation type.
/// </summary>
/// <param name="ServiceType">The service contract type.</param>
/// <param name="ImplementationType">The implementation type, if different from the service type.</param>
public record ServiceInfo(Type ServiceType, Type? ImplementationType)
{
    /// <summary>
    /// Initializes a new instance from a tuple representation.
    /// </summary>
    /// <param name="tuple">The tuple containing service and implementation types.</param>
    public ServiceInfo(ServiceInfoTuple tuple) : this(tuple.ServiceType, tuple.ImplementationType)
    {
    }

    /// <summary>
    /// Gets the resolved implementation type, falling back to <see cref="ServiceType"/> when null.
    /// </summary>
    public Type ImplementationType { get; init; } = ImplementationType ?? ServiceType;
}
