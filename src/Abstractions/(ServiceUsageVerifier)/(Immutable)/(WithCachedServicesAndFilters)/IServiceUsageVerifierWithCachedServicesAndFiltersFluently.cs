using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedServicesAndFiltersFluently
    : IServiceUsageVerifierWithCachedFiltersFluently, IServiceUsageVerifierWithCachedServicesFluently
{
    /// <summary>
    /// Finds unused services using cached services and filters plus optional one-time inputs.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices,
        in TRootServicesCollection? oneTimeRootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    /// <summary>
    /// Finds unused services using cached services and filters for a single services collection.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServices<TAllServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull =>
        FindUnusedServices<TAllServicesCollection, object>(out unusedServices, oneTimeAllServices, null,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

    /// <summary>
    /// Finds unused services using cached services and filters with only one-time filters.
    /// </summary>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServicesFilters">Optional one-time filters for all services.</param>
    /// <param name="oneTimeRootServicesFilters">Optional one-time filters for root services.</param>
    /// <param name="oneTimeUnusedServicesFilters">Optional one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServices(out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null) =>
        FindUnusedServices<object>(out unusedServices, null,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

    /// <summary>
    /// Finds unused services using cached services and all-services one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeAllServicesFilters">The one-time filters for all services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeAllServicesFilters);

    /// <summary>
    /// Finds unused services using cached services and root-services one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeRootServicesFilters">The one-time filters for root services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeRootServicesFilters);

    /// <summary>
    /// Finds unused services using cached services and unused-services one-time filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="unusedServices">The resulting unused services.</param>
    /// <param name="oneTimeAllServices">Optional one-time all-services collection.</param>
    /// <param name="oneTimeRootServices">Optional one-time root-services collection.</param>
    /// <param name="oneTimeUnusedServicesFilters">The one-time filters for unused services.</param>
    /// <returns>The current verifier instance.</returns>
    IServiceUsageVerifier FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeUnusedServicesFilters);
}
