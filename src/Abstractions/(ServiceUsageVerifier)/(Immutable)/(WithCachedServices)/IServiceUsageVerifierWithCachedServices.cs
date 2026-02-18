using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedServices
    : IServiceUsageVerifierWithCachedServicesFluently, IServiceUsageVerifierWithoutCaches
{
    /// <summary>
    /// Finds unused services using cached services and optional one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="allServicesFilters">Optional filters for all services.</param>
    /// <param name="rootServicesFilters">Optional filters for root services.</param>
    /// <param name="unusedServicesFilters">Optional filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeFilters(out var unusedServices, in oneTimeAllServices, in oneTimeRootServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }
}
