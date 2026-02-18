namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithCachedServicesMutable
    : IServiceUsageVerifierWithCachedServices, IServiceUsageVerifierWithoutCachesMutable
{
    new IServiceUsageVerifierWithCachedServices AsImmutable() => this;
}