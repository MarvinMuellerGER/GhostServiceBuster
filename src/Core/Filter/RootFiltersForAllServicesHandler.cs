using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal sealed class RootFiltersForAllServicesHandler(IFilterHandler filterHandler) : IFilterHandler
{
    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? filters) =>
        filters is null || filters.Count is 0
            ? []
            : filterHandler.ApplyFilters(serviceInfo, filters);
}