using GhostServiceBuster.Default;
using GhostServiceBuster.MS.Detect;
using GhostServiceBuster.MS.Extract;

namespace GhostServiceBuster.MS;

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier ForServiceCollection(this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.Default()
            .RegisterServiceCollectionServiceInfoExtractor()
            .RegisterServiceProviderUsageDetector();
}