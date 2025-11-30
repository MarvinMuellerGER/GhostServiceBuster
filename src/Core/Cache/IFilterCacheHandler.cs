using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal interface IFilterCacheHandler : IFilterHandler
{
    void RegisterFilters(ServiceInfoFilterInfoList filters);

    void RegisterFilters(params IReadOnlyList<IServiceInfoFilter> filters) =>
        RegisterFilters(filters.Select(f => new ServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual))
            .ToServiceInfoFilterInfoList());

    void RegisterFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        RegisterFilters(new TServiceInfoFilter());

    new ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters = null);

    new ServiceInfoSet ApplyFilters(
        ServiceInfoSet serviceInfo, params IReadOnlyList<IServiceInfoFilter> oneTimeFilters) =>
        ((IFilterHandler)this).ApplyFilters(serviceInfo, oneTimeFilters);

    event EventHandlerWithoutParameters NewFiltersRegistered;
}