namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public partial interface IServiceUsageVerifierWithCachedServicesAndFiltersMutable
    : IServiceUsageVerifierWithCachedServicesAndFilters,
        IServiceUsageVerifierWithCachedFiltersMutable,
        IServiceUsageVerifierWithCachedServicesMutable
{
    new IServiceUsageVerifierWithCachedServicesAndFilters AsImmutable() => this;
}