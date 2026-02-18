using System.Collections.Immutable;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Collections;

partial class ServiceInfoSet
{
    /// <summary>
    /// Converts an immutable hash set to a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <param name="set">The set to convert.</param>
    public static implicit operator ServiceInfoSet(ImmutableHashSet<ServiceInfo> set) => new(set);

    /// <summary>
    /// Converts a list to a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <param name="set">The list to convert.</param>
    public static implicit operator ServiceInfoSet(List<ServiceInfo> set) => set.ToImmutableHashSet();

    /// <summary>
    /// Converts a service info instance to a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <param name="serviceInfo">The service info to convert.</param>
    public static implicit operator ServiceInfoSet(ServiceInfo? serviceInfo) =>
        serviceInfo is null ? Empty : [serviceInfo];

    /// <summary>
    /// Converts a service info tuple to a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <param name="serviceInfoTuple">The tuple to convert.</param>
    public static implicit operator ServiceInfoSet(ServiceInfoTuple? serviceInfoTuple) =>
        serviceInfoTuple.HasValue ? new ServiceInfo(serviceInfoTuple.Value) : [];
}
