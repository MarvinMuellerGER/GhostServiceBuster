using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal interface IRootFilterHandler
{
    ServiceInfoSet ApplyFilters(ServiceInfoSet allServices, ServiceInfoSet rootServices,
        ServiceInfoFilterInfoList? filters);

    ServiceInfoSet ApplyFilters(
        ServiceInfoSet allServices, ServiceInfoSet rootServices, IReadOnlyList<IServiceInfoFilter> filters) =>
        ApplyFilters(allServices, rootServices, filters.ToServiceInfoFilterInfoList());

    ServiceInfoSet ApplyFilter<TServiceInfoFilter>(ServiceInfoSet allServices, ServiceInfoSet rootServices)
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        ApplyFilters(allServices, rootServices, [new TServiceInfoFilter()]);
}