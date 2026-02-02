using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.MS.Generator;
using GhostServiceBuster.RegisterMethodsGenerator;

namespace GhostServiceBuster.MS.Filter;

[GenerateRegisterMethodFor]
internal sealed class ServiceProviderUsageFilter : IRootServiceInfoFilter
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