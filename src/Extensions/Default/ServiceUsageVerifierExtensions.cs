namespace GhostServiceBuster.Default;

/// <summary>
/// Provides default configuration extensions for service usage verifiers.
/// </summary>
public static class ServiceUsageVerifierExtensions
{
    /// <summary>
    /// Registers the default extractor and dependency detector.
    /// </summary>
    /// <typeparam name="TServiceUsageVerifier">The verifier type.</typeparam>
    /// <param name="serviceUsageVerifier">The verifier to configure.</param>
    /// <returns>The configured verifier.</returns>
    public static TServiceUsageVerifier Default<TServiceUsageVerifier>(
        this TServiceUsageVerifier serviceUsageVerifier)
        where TServiceUsageVerifier : IServiceUsageVerifierWithoutCachesMutable =>
        serviceUsageVerifier.RegisterTypeServiceInfoExtractor().RegisterConstructorInjectionDetector();
}
