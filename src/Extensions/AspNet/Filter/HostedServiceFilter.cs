using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Microsoft.Extensions.Hosting;

namespace GhostServiceBuster.AspNet.Filter;

file sealed class HostedServiceFilter : IRootServiceInfoFilter
{
    public bool IsIndividual => true;

    public bool UseAllServices => true;

    public ServiceInfoSet GetFilteredServices(ServiceInfoSet serviceInfos) =>
        serviceInfos.Where(s => s.ServiceType == typeof(IHostedService));
}

public static partial class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier RegisterHostedServiceRootServicesFilter(
        this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterRootServicesFilter<HostedServiceFilter>();
}