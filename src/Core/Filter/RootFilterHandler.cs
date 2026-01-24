using GhostServiceBuster.Collections;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Filter;

internal sealed class RootFilterHandler(
    IFilterHandler filterHandler,
    [Tag(RootFiltersForAllServices)] IFilterHandler rootFiltersForAllServicesHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootFilterHandler
{
    public ServiceInfoSet ApplyFilters(
        ServiceInfoSet allServices, ServiceInfoSet rootServices, ServiceInfoFilterInfoList? filters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) =
            rootFilterSplitter.SplitFilters(filters);

        return filterHandler.ApplyFilters(rootServices, rootFiltersForRootServices)
            .Concat(rootFiltersForAllServicesHandler.ApplyFilters(allServices, rootFiltersForAllServices));
    }
}