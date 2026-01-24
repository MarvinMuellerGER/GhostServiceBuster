using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster.Cache;

internal sealed class RootServiceAndFilterCacheHandler(
    [Tag(RootServicesWithRootFilters)] IServiceAndFilterCacheHandler rootServiceAndRootFilterCacheHandler,
    [Tag(AllServicesWithRootFilters)] IServiceAndFilterCacheHandler allServiceAndRootFilterCacheHandler,
    IRootFilterSplitter rootFilterSplitter)
    : IRootServiceAndFilterCacheHandler
{
    public bool NewServicesOrFiltersRegisteredSinceLastGet =>
        rootServiceAndRootFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet ||
        allServiceAndRootFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet;

    public ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices,
        in ServiceInfoFilterInfoList? oneTimeFilters)
        where TServiceCollection : notnull =>
        GetFilteredServices<object, TServiceCollection>(null, oneTimeServices, oneTimeFilters);

    public ServiceInfoSet GetFilteredServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices,
        in TRootServicesCollection? oneTimeRootServices,
        in ServiceInfoFilterInfoList? oneTimeFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        var (rootFiltersForAllServices, rootFiltersForRootServices) =
            rootFilterSplitter.SplitFilters(oneTimeFilters);

        var serviceInfoSet =
            allServiceAndRootFilterCacheHandler.GetFilteredServices(oneTimeAllServices, rootFiltersForAllServices);

        var filteredServices = rootServiceAndRootFilterCacheHandler
            .GetFilteredServices(oneTimeRootServices, rootFiltersForRootServices);

        return serviceInfoSet.Concat(filteredServices);
    }
}