using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS.Filter;

file sealed class ServiceCollectionInstanceFilter : IServiceInfoFilter
{
    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => s.ServiceType != typeof(IServiceCollection));
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterServiceCollectionInstanceUnusedServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterUnusedServicesFilter<ServiceCollectionInstanceFilter>();
}