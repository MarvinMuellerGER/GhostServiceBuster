using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

/// <summary>
/// Applies root filters that target all services.
/// </summary>
internal sealed class RootFiltersForAllServicesHandler(IFilterHandler filterHandler) : IFilterHandler
{
    /// <summary>
    /// Applies filters to the provided services.
    /// </summary>
    /// <param name="serviceInfo">The services to filter.</param>
    /// <param name="filters">The filters to apply.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? filters) =>
        filters is null || filters.Count is 0
            ? []
            : filterHandler.ApplyFilters(serviceInfo, filters);
}
