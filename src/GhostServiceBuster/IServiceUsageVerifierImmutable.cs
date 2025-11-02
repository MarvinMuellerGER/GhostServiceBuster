namespace GhostServiceBuster;

public interface IServiceUsageVerifierImmutable :
    IServiceUsageVerifierWithoutCaches,
    IServiceUsageVerifierWithCachedServices,
    IServiceUsageVerifierWithCachedFilters,
    IServiceUsageVerifierWithCachedServicesAndFilters;