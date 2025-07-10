using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal interface IFilterHandler
{
    ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList filters);
}