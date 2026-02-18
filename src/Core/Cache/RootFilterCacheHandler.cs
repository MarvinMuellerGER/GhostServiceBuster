using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Cache;

/// <summary>
/// Applies root filters across all services and root services.
/// </summary>
internal sealed class RootFilterCacheHandler(
    [Tag(RootFiltersForRootServices)] IFilterCacheHandler rootFiltersForRootServicesCacheHandler,
    [Tag(RootFiltersForAllServices)] IFilterCacheHandler rootFiltersForAllServicesCacheHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootFilterCacheHandler
{
    /// <summary>
    /// Registers root filters for subsequent applications.
    /// </summary>
    /// <param name="filters">The filters to register.</param>
    public void RegisterFilters(ServiceInfoFilterInfoList? filters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) = rootFilterSplitter.SplitFilters(filters);

        rootFiltersForAllServicesCacheHandler.RegisterFilters(rootFiltersForAllServices);
        rootFiltersForRootServicesCacheHandler.RegisterFilters(rootFiltersForRootServices);
    }

    /// <summary>
    /// Applies root filters to the provided service sets.
    /// </summary>
    /// <param name="allServices">The set of all services.</param>
    /// <param name="rootServices">The set of root services.</param>
    /// <param name="oneTimeFilters">Optional one-time filters.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet ApplyFilters(
        ServiceInfoSet allServices, ServiceInfoSet rootServices, ServiceInfoFilterInfoList? oneTimeFilters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) = rootFilterSplitter.SplitFilters(oneTimeFilters);

        return rootFiltersForRootServicesCacheHandler.ApplyFilters(rootServices, rootFiltersForRootServices)
            .Concat(rootFiltersForAllServicesCacheHandler.ApplyFilters(allServices, rootFiltersForAllServices));
    }
}
