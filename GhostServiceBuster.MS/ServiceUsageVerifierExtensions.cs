using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Extract;
using GhostServiceBuster.MS.Filter;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier ForServiceCollection(this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.Default()
            .RegisterServiceCollectionServiceInfoExtractor()
            .RegisterServiceProviderUsageRootServicesFilter()
            .UseAllServicesAsRootServices();
}