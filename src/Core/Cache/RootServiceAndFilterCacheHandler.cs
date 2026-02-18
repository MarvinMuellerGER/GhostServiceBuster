using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Cache;

/// <summary>
/// Combines root service and root filter caches for filtering services.
/// </summary>
internal sealed class RootServiceAndFilterCacheHandler(
    [Tag(RootServicesWithRootFilters)] IServiceAndFilterCacheHandler rootServiceAndRootFilterCacheHandler,
    [Tag(AllServicesWithRootFilters)] IServiceAndFilterCacheHandler allServiceAndRootFilterCacheHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootServiceAndFilterCacheHandler
{
    /// <summary>
    /// Gets whether services or filters were registered since the last retrieval.
    /// </summary>
    public bool NewServicesOrFiltersRegisteredSinceLastGet =>
        rootServiceAndRootFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet ||
        allServiceAndRootFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet;

    /// <summary>
    /// Gets filtered services using a root services collection.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="oneTimeServices">Optional one-time root services.</param>
    /// <param name="oneTimeFilters">Optional one-time filters.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices,
        in ServiceInfoFilterInfoList? oneTimeFilters)
        where TServiceCollection : notnull =>
        GetFilteredServices<object, TServiceCollection>(null, oneTimeServices, oneTimeFilters);

    /// <summary>
    /// Gets filtered services using optional one-time all and root services plus filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="oneTimeAllServices">Optional one-time all services.</param>
    /// <param name="oneTimeRootServices">Optional one-time root services.</param>
    /// <param name="oneTimeFilters">Optional one-time filters.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet GetFilteredServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices,
        in TRootServicesCollection? oneTimeRootServices,
        in ServiceInfoFilterInfoList? oneTimeFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) =
            rootFilterSplitter.SplitFilters(oneTimeFilters);

        var serviceInfoSet =
            allServiceAndRootFilterCacheHandler.GetFilteredServices(oneTimeAllServices, rootFiltersForAllServices);

        var filteredServices = rootServiceAndRootFilterCacheHandler
            .GetFilteredServices(oneTimeRootServices, rootFiltersForRootServices);

        return serviceInfoSet.Concat(filteredServices);
    }
}
