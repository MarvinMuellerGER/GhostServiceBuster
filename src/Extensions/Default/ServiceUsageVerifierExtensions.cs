using GhostServiceBuster.Default.Detect;
using GhostServiceBuster.Default.Extract;

namespace GhostServiceBuster.Default;

public static class ServiceUsageVerifierExtensions
{
    public static IServiceUsageVerifier Default(this IServiceUsageVerifier serviceUsageVerifier) =>
        serviceUsageVerifier.RegisterTypeServiceInfoExtractor().RegisterConstructorInjectionDetector();
}