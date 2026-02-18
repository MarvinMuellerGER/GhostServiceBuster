using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedFilters
    : IServiceUsageVerifierWithCachedFiltersFluently, IServiceUsageVerifierWithoutCaches
{
    /// <summary>
    /// Finds unused services using one-time services and optional one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServices(in allServices, in rootServices, out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time all-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="oneTimeAllServicesFilters">The one-time filters for all services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeAllServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time root-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="oneTimeRootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeRootServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using one-time services and one-time unused-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The one-time collection of all services.</param>
    /// <param name="rootServices">The one-time collection of root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeUnusedServicesFilters);

        return unusedServices;
    }
}
