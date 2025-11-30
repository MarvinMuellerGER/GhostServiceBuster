using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal interface IFilterHandler
{
    ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? filters);

    ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, params IReadOnlyList<IServiceInfoFilter> filters) =>
        ApplyFilters(serviceInfo,
            filters.Select(f => new ServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual))
                .ToServiceInfoFilterInfoList());

    ServiceInfoSet ApplyFilter<TServiceInfoFilter>(ServiceInfoSet serviceInfo)
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        ApplyFilters(serviceInfo, new TServiceInfoFilter());
}