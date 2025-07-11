using System.Collections.Immutable;
using GhostServiceBuster.Core;

namespace GhostServiceBuster.Collections;

partial class ServiceInfoSet
{
    public static implicit operator ServiceInfoSet(ImmutableHashSet<ServiceInfo> set) => new(set);
    
    public static implicit operator ServiceInfoSet(List<ServiceInfo> set) => set.ToImmutableHashSet();

    public static implicit operator ServiceInfoSet(ServiceInfo? serviceInfo) =>
        serviceInfo is null ? Empty : [serviceInfo];
}