using GhostServiceBuster.Collections;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Filter;

/// <summary>
/// Applies root filters across all services and root services.
/// </summary>
internal sealed class RootFilterHandler(
    IFilterHandler filterHandler,
    [Tag(RootFiltersForAllServices)] IFilterHandler rootFiltersForAllServicesHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootFilterHandler
{
    /// <summary>
    /// Applies filters to the provided service sets.
    /// </summary>
    /// <param name="allServices">The set of all services.</param>
    /// <param name="rootServices">The set of root services.</param>
    /// <param name="filters">The filters to apply.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet ApplyFilters(
        ServiceInfoSet allServices, ServiceInfoSet rootServices, ServiceInfoFilterInfoList? filters)
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) =
            rootFilterSplitter.SplitFilters(filters);

        return filterHandler.ApplyFilters(rootServices, rootFiltersForRootServices)
            .Concat(rootFiltersForAllServicesHandler.ApplyFilters(allServices, rootFiltersForAllServices));
    }
}
