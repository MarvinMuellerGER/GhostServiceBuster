using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedServicesAndFilters
    : IServiceUsageVerifierWithCachedServicesAndFiltersFluently,
        IServiceUsageVerifierWithCachedFilters, IServiceUsageVerifierWithCachedServices
{
    /// <summary>
    /// Finds unused services using cached services and filters plus optional one-time inputs.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServices(out var unusedServices, in oneTimeAllServices,
            in oneTimeRootServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using cached services and filters for a single services collection.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServices<TAllServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
    {
        FindUnusedServices(out var unusedServices, in oneTimeAllServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using cached services and filters with only one-time filters.
    /// </summary>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServices(
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
    {
        FindUnusedServices(out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using cached services and one-time all-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">The one-time filters for all services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithAllServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeAllServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using cached services and one-time root-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeRootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithRootServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeRootServicesFilters);

        return unusedServices;
    }

    /// <summary>
    /// Finds unused services using cached services and one-time unused-services filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeUnusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The unused services.</returns>
    ServiceInfoSet FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithUnusedServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeUnusedServicesFilters);

        return unusedServices;
    }
}
