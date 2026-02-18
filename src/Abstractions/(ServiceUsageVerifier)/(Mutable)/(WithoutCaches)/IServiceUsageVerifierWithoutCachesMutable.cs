namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithoutCachesMutable : IServiceUsageVerifierWithoutCaches
{
    /// <summary>
    /// Casts this instance of <see cref="IServiceUsageVerifier"/> to an immutable version.
    /// </summary>
    IServiceUsageVerifierWithoutCaches AsImmutable() => this;
}