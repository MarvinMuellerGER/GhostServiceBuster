using System.Collections.Frozen;
using System.Reflection;
using GhostServiceBuster.AspNet.Utils;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using GhostServiceBuster.RegisterMethodsGenerator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.AspNet.Filter;

/// <summary>
/// Defines that every service injected into a minimal api endpoint should be treated as a root service.
/// </summary>
/// <param name="services">The service provider used to retrieve information about minimal API endpoints.</param>
[GenerateRegisterMethodFor]
internal sealed class MinimalApiInjectionFilter(IServiceProvider services) : IRootServiceInfoFilter
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