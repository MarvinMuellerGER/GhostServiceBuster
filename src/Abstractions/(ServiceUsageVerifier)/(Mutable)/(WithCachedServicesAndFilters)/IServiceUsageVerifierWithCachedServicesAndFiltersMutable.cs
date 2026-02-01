namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesAndFiltersMutable
    : IServiceUsageVerifierWithCachedServicesAndFilters,
        IServiceUsageVerifierWithCachedFiltersMutable,
        IServiceUsageVerifierWithCachedServicesMutable
{
    new IServiceUsageVerifierWithCachedServicesAndFilters AsImmutable() => this;
}