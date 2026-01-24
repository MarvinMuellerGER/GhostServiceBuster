using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal interface IRootFilterSplitter
{
    (ServiceInfoFilterInfoList? rootFiltersForAllServices, ServiceInfoFilterInfoList? rootFiltersForRootServices)
        SplitFilters(ServiceInfoFilterInfoList? filters);
}