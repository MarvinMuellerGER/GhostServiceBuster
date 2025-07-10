using System.Collections.Immutable;
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal sealed class FilterHandler : IFilterHandler
{
    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList filters) =>
        filters.Count is 0
            ? serviceInfo
            : ApplyNonIndividualFilters(serviceInfo, filters)
                .Concat(ApplyIndividualFilters(serviceInfo, filters)).ToImmutableHashSet();

    private static ServiceInfoSet ApplyNonIndividualFilters(
        ServiceInfoSet serviceInfo, IReadOnlyList<ServiceInfoFilterInfo> filters) =>
        filters.All(filter => filter.IsIndividual)
            ? []
            : filters
                .Where(filter => !filter.IsIndividual)
                .Aggregate(serviceInfo, (current, filter) => filter.Filter.Invoke(current));

    private static ServiceInfoSet ApplyIndividualFilters(
        ServiceInfoSet serviceInfo, IEnumerable<ServiceInfoFilterInfo> filters)
        => filters
            .Where(filter => filter.IsIndividual)
            .SelectMany(filter => filter.Filter.Invoke(serviceInfo)).ToImmutableHashSet();
}