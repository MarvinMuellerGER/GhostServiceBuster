using System.Collections.Immutable;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal sealed class FilterCacheHandler(IFilterHandler filterHandler) : IFilterCacheHandler
{
    private readonly List<ServiceInfoFilterInfo> _filters = [];

    public void RegisterFilters(ServiceInfoFilterInfoList filters) => _filters.AddRange(filters);

    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters) =>
        filterHandler.ApplyFilters(serviceInfo, _filters.Concat(oneTimeFilters ?? []).ToImmutableList());
}