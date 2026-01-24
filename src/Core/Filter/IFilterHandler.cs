using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal interface IFilterHandler
{
    ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? filters);

    ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, IReadOnlyList<IServiceInfoFilter> filters) =>
        ApplyFilters(serviceInfo, filters.ToServiceInfoFilterInfoList());

    ServiceInfoSet ApplyFilter<TServiceInfoFilter>(ServiceInfoSet serviceInfo)
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        ApplyFilters(serviceInfo, [new TServiceInfoFilter()]);
}