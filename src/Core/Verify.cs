namespace GhostServiceBuster;

/// <summary>
/// Entry point for creating a new <see cref="IServiceUsageVerifier"/>.
/// </summary>
public static class Verify
{
    /// <summary>
    /// Creates a new <see cref="IServiceUsageVerifier"/>.
    /// </summary>
    public static IServiceUsageVerifierWithoutCachesMutable New => Composition.Instance.ServiceUsageVerifier;
}