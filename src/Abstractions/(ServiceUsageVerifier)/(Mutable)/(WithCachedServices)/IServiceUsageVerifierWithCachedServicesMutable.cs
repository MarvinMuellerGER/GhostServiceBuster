namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithCachedServicesMutable
    : IServiceUsageVerifierWithCachedServices, IServiceUsageVerifierWithoutCachesMutable
{
    /// <summary>
    /// Returns an immutable view of the verifier.
    /// </summary>
    /// <returns>The immutable verifier view.</returns>
    new IServiceUsageVerifierWithCachedServices AsImmutable() => this;
}
