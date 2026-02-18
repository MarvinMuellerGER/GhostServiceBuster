using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

/// <summary>
/// Applies service info filters to a set of services.
/// </summary>
internal sealed class FilterHandler : IFilterHandler
{
    /// <summary>
    /// Applies filters to the provided services.
    /// </summary>
    /// <param name="serviceInfo">The services to filter.</param>
    /// <param name="filters">The filters to apply.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? filters)
    {
        if (filters is null || filters.Count is 0)
            return serviceInfo;

        return ApplyNonIndividualFilters(serviceInfo, filters).Concat(ApplyIndividualFilters(serviceInfo, filters));
    }

    private static ServiceInfoSet ApplyNonIndividualFilters(
        ServiceInfoSet serviceInfo, IReadOnlyList<ServiceInfoFilterInfo> filters)
    {
        return filters.All(filter => filter.IsIndividual)
            ? []
            : filters
                .Where(filter => !filter.IsIndividual)
                .Aggregate(serviceInfo, (current, filter) => filter.Filter.Invoke(current));
    }

    private static ServiceInfoSet ApplyIndividualFilters(
        ServiceInfoSet serviceInfo, IEnumerable<ServiceInfoFilterInfo> filters) =>
        filters.Where(filter => filter.IsIndividual).SelectMany(filter => filter.Filter.Invoke(serviceInfo));
}
