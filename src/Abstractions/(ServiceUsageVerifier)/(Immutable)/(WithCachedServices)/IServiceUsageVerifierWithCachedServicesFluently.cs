using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc/>
public interface IServiceUsageVerifierWithCachedServicesFluently : IServiceUsageVerifierWithoutCachesFluently
{
    /// <summary>
    /// Finds unused services using cached services and optional one-time filters, returning the verifier for fluent use.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="allServicesFilters">Optional filters for all services.</param>
    /// <param name="rootServicesFilters">Optional filters for root services.</param>
    /// <param name="unusedServicesFilters">Optional filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;
}
