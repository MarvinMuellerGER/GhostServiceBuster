using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.MS.Generator;

namespace GhostServiceBuster.MS.Filter;

file sealed class ServiceProviderUsageFilter : IServiceInfoFilter
{
    private FrozenSet<Type>? _typesResolvedByServiceProvider;

    public bool IsIndividual => true;

    private FrozenSet<Type> TypesResolvedByServiceProvider
    {
        get
        {
            _typesResolvedByServiceProvider ??= AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                    a.GetCustomAttributes<TypesResolvedByServiceProviderAttribute>().SelectMany(ca => ca.Types))
                .ToFrozenSet();

            return _typesResolvedByServiceProvider;
        }
    }

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => TypesResolvedByServiceProvider.Contains(s.ServiceType));
}

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterServiceProviderUsageRootServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<ServiceProviderUsageFilter>();
}