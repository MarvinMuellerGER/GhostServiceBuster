using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc/>
public interface IServiceUsageVerifierWithoutCachesFluently : IServiceUsageVerifier
{
    /// <summary>
    /// Finds unused services using one-time services and one-time filters, returning the verifier for fluent use.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="allServicesFilters">Optional filters for all services.</param>
    /// <param name="rootServicesFilters">Optional filters for root services.</param>
    /// <param name="unusedServicesFilters">Optional filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
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
    /// <param name="allServicesFilters">The one-time filters for all services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, allServicesFilters);

    /// <summary>
    /// Finds unused services using one-time services and one-time root-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="rootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, rootServicesFilters: rootServicesFilters);

    /// <summary>
    /// Finds unused services using one-time services and one-time unused-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="unusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, unusedServicesFilters: unusedServicesFilters);
}
