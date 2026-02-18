using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc/>
public interface IServiceUsageVerifierWithCachedFiltersFluently : IServiceUsageVerifierWithoutCachesFluently
{
    /// <summary>
    /// Finds unused services using one-time services and optional one-time filters, returning the verifier for fluent use.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    /// <summary>
    /// Finds unused services using one-time services and one-time all-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServicesFilters">The one-time filters for all services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeAllServicesFilters);

    /// <summary>
    /// Finds unused services using one-time services and one-time root-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeRootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeRootServicesFilters);

    /// <summary>
    /// Finds unused services using one-time services and one-time unused-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeUnusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeUnusedServicesFilters);
}
