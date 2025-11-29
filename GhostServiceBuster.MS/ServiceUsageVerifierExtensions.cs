using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Extract;
using GhostServiceBuster.MS.Filter;
using Microsoft.Extensions.DependencyInjection;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier ForServiceCollection(
        this IServiceUsageVerifier serviceUsageVerifier, IServiceProvider services) =>
        serviceUsageVerifier.ForServiceCollection()
            .RegisterAllServices(services);

    public static IServiceUsageVerifier ForServiceCollection(
        this IServiceUsageVerifier serviceUsageVerifier, IServiceCollection services) =>
        serviceUsageVerifier.ForServiceCollection()
            .RegisterAllServices(services);

    public static IServiceUsageVerifier ForServiceCollection(this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.Default()
            .RegisterServiceCollectionServiceInfoExtractor()
            .RegisterServiceProviderUsageRootServicesFilter()
            .UseAllServicesAsRootServices();
}