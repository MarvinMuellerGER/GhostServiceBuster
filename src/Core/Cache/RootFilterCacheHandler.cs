using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Cache;

internal sealed class RootFilterCacheHandler(
    [Tag(RootFiltersForRootServices)] IFilterCacheHandler rootFiltersForRootServicesCacheHandler,
    [Tag(RootFiltersForAllServices)] IFilterCacheHandler rootFiltersForAllServicesCacheHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootFilterCacheHandler
{
    public void RegisterFilters(ServiceInfoFilterInfoList? filters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) = rootFilterSplitter.SplitFilters(filters);

        rootFiltersForAllServicesCacheHandler.RegisterFilters(rootFiltersForAllServices);
        rootFiltersForRootServicesCacheHandler.RegisterFilters(rootFiltersForRootServices);
    }

    public ServiceInfoSet ApplyFilters(
        ServiceInfoSet allServices, ServiceInfoSet rootServices, ServiceInfoFilterInfoList? oneTimeFilters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) = rootFilterSplitter.SplitFilters(oneTimeFilters);

        return rootFiltersForRootServicesCacheHandler.ApplyFilters(rootServices, rootFiltersForRootServices)
            .Concat(rootFiltersForAllServicesCacheHandler.ApplyFilters(allServices, rootFiltersForAllServices));
    }
}