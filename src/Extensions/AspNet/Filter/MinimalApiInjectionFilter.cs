using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.AspNet.Utils;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet.Filter;

file sealed class MinimalApiInjectionFilter(IServiceProvider services) : IRootServiceInfoFilter
{
    private FrozenSet<Type> TypesInjectedIntoMinimalApi =>
        field ??= services.GetRequiredService<EndpointDataSource>()
            .Endpoints
            .OfType<RouteEndpoint>()
            .SelectMany(e => e.Metadata).OfType<MethodInfo>()
            .SelectMany(m => m.GetParameters())
            .Where(p => p.GetCustomAttributes<IBindingSourceMetadata>()
                .All(bm => bm.BindingSource == BindingSource.Services))
            .Select(p => p.ParameterType)
            .ToFrozenSet();

    public bool IsIndividual => true;

    public bool UseAllServices => true;

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => TypesInjectedIntoMinimalApi.Contains(s.ServiceType));
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifierWithCachedFiltersMutable RegisterMinimalApiInjectionRootServicesFilter(
        this IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier, IServiceProvider services) =>
        serviceUsageVerifier.RegisterRootServicesFilter(new MinimalApiInjectionFilter(services));

    public static IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterMinimalApiInjectionRootServicesFilter(
            this IServiceUsageVerifierWithCachedServicesMutable serviceUsageVerifier, IServiceProvider services) =>
        serviceUsageVerifier.RegisterRootServicesFilter(new MinimalApiInjectionFilter(services));
}