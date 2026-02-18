using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc/>
public interface IServiceUsageVerifierWithoutCaches : IServiceUsageVerifierWithoutCachesFluently
{
    /// <summary>
    /// Finds unused services using one-time services and optional one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="allServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="rootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="unusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(in allServices, in rootServices, out var unusedServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time all-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="allServicesFilters">The one-time filters for all services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters(
            in allServices, in rootServices, out var unusedServices, allServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time root-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="rootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters(
            in allServices, in rootServices, out var unusedServices, rootServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time unused-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="unusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters(
            in allServices, in rootServices, out var unusedServices, unusedServicesFilters);

        return unusedServices;
    }
}
