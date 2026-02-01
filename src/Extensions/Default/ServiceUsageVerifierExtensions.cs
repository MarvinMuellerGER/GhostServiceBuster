using GhostServiceBuster.Default.Detect;
using GhostServiceBuster.Default.Extract;

namespace GhostServiceBuster.Default;

public static class ServiceUsageVerifierExtensions
{
    public static TServiceUsageVerifier Default<TServiceUsageVerifier>(
        this TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable =>
        serviceUsageVerifier.RegisterTypeServiceInfoExtractor().RegisterConstructorInjectionDetector();
}