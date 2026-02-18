using System.Collections.Immutable;
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

/// <summary>
/// Splits root filters into all-services and root-services groups.
/// </summary>
internal sealed class RootFilterSplitter : IRootFilterSplitter
{
    /// <summary>
    /// Splits the provided filters into all-services and root-services filters.
    /// </summary>
    /// <param name="filters">The filters to split.</param>
    /// <returns>The split filter lists.</returns>
    public (ServiceInfoFilterInfoList? rootFiltersForAllServices, ServiceInfoFilterInfoList? rootFiltersForRootServices)
        SplitFilters(ServiceInfoFilterInfoList? filters)
    {
        if (filters is null)
            return (null, null);

        var rootServiceInfoFilterInfos = filters.OfType<RootServiceInfoFilterInfo>().ToList();
        var normalServiceInfoFilterInfos = filters.Except(rootServiceInfoFilterInfos);

        var rootFiltersForAllServices = rootServiceInfoFilterInfos.Where(f => f.UseAllServices)
            .Cast<ServiceInfoFilterInfo>().ToImmutableList();

        var rootFiltersForRootServices = rootServiceInfoFilterInfos.Except(rootFiltersForAllServices)
            .Concat(normalServiceInfoFilterInfos);

        return (rootFiltersForAllServices, rootFiltersForRootServices);
    }
}
