using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.MS.Generator;

namespace GhostServiceBuster.MS.Filter;

file sealed class ServiceProviderUsageFilter : IRootServiceInfoFilter
{
    private FrozenSet<Type> TypesResolvedByServiceProvider =>
        field ??= AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                a.GetCustomAttributes<TypesResolvedByServiceProviderAttribute>().SelectMany(ca => ca.Types))
            .ToFrozenSet();

    public bool IsIndividual => true;

    public bool UseAllServices => true;

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => TypesResolvedByServiceProvider.Contains(s.ServiceType));
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceProviderUsageRootServicesFilter(
        this IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<ServiceProviderUsageFilter>();

    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterServiceProviderUsageRootServicesFilter(
            this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<ServiceProviderUsageFilter>();
}