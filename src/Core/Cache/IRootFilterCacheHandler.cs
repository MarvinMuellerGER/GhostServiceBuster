using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal interface IRootFilterCacheHandler : IFilterRegisterHandler
{
    ServiceInfoSet ApplyFilters(ServiceInfoSet allServices, ServiceInfoSet rootServices,
        ServiceInfoFilterInfoList? oneTimeFilters);
}