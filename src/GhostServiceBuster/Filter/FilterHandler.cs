using System.Collections.Immutable;
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal sealed class FilterHandler : IFilterHandler
{
    public ServiceInfoSet ApplyFilters(
        ServiceInfoSet serviceInfo,
        ServiceInfoFilterInfoList? filters,
        ServiceInfoFilterInfo? oneTimeFilter = null)
    {
        var notNullFilters = filters ?? [];
        var filtersInclOneTimeFilter =
            (oneTimeFilter is null ? notNullFilters : notNullFilters.Append(oneTimeFilter)).ToList();

        return filtersInclOneTimeFilter.Count is 0
            ? serviceInfo
            : ApplyNonIndividualFilters(serviceInfo, filtersInclOneTimeFilter)
                .Concat(ApplyIndividualFilters(serviceInfo, filtersInclOneTimeFilter)).ToImmutableHashSet();
    }

    private static ServiceInfoSet ApplyNonIndividualFilters(ServiceInfoSet serviceInfo,
        IReadOnlyList<ServiceInfoFilterInfo> filters) =>
        filters.All(filter => filter.IsIndividual)
            ? []
            : filters
                .Where(filter => !filter.IsIndividual)
                .Aggregate(serviceInfo, (current, filter) => filter.Filter.Invoke(current));

    private static ServiceInfoSet ApplyIndividualFilters(ServiceInfoSet serviceInfo,
        IEnumerable<ServiceInfoFilterInfo> filters)
        => filters
            .Where(filter => filter.IsIndividual)
            .SelectMany(filter => filter.Filter.Invoke(serviceInfo)).ToImmutableHashSet();
}