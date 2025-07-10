using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal interface IFilterCacheHandler : IFilterHandler
{
    void RegisterFilters(ServiceInfoFilterInfoList filters);

    new ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters = null);
}