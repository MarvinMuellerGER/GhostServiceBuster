using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.AspNet.Generator;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.AspNet.Filter;

file sealed class MinimalApiInjectionFilter : IRootServiceInfoFilter
{
    private FrozenSet<Type> TypesInjectedIntoMinimalApi =>
        field ??= AppDomain.CurrentDomain.GetAssemblies().SelectMany(a =>
                a.GetCustomAttributes<TypesInjectedIntoMinimalApiAttribute>().SelectMany(ca => ca.Types))
            .ToFrozenSet();

    public bool IsIndividual => true;

    public bool UseAllServices => true;

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => TypesInjectedIntoMinimalApi.Contains(s.ServiceType));
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterMinimalApiInjectionRootServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<MinimalApiInjectionFilter>();
}