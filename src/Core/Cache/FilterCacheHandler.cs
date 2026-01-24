using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal sealed class FilterCacheHandler(IFilterHandler filterHandler) : IFilterCacheHandler
{
    private readonly List<ServiceInfoFilterInfo> _filters = [];

    public event EventHandlerWithoutParameters? NewFiltersRegistered;

    public void RegisterFilters(ServiceInfoFilterInfoList? filters)
    {
        if (filters is null)
            return;

        _filters.AddRange(filters);
        NewFiltersRegistered?.Invoke();
    }

    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters) =>
        filterHandler.ApplyFilters(serviceInfo, _filters.Concat(oneTimeFilters));
}