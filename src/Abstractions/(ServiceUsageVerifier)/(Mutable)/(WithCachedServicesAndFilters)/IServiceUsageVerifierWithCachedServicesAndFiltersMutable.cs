namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithCachedServicesAndFiltersMutable
    : IServiceUsageVerifierWithCachedServicesAndFilters,
        IServiceUsageVerifierWithCachedFiltersMutable,
        IServiceUsageVerifierWithCachedServicesMutable
{
    /// <summary>
    /// Returns an immutable view of the verifier.
    /// </summary>
    /// <returns>The immutable verifier view.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFilters AsImmutable() => this;
}
