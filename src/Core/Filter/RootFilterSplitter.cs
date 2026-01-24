using System.Collections.Immutable;
using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Filter;

internal sealed class RootFilterSplitter : IRootFilterSplitter
{
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