using GhostServiceBuster.Cache;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;
using Pure.DI;
using static Pure.DI.Tag;

namespace GhostServiceBuster;

internal sealed partial class ServiceUsageVerifier(
    IUnusedServiceDetector unusedServiceDetector,
    IServiceInfoExtractorHandler serviceInfoExtractorHandler,
    IFilterHandler filterHandler,
    [Tag(All)] IServiceCacheHandler allServicesCacheHandler,
    [Tag(Root)] IServiceCacheHandler rootServicesCacheHandler,
    [Tag(Unused)] IServiceCacheHandler unusedServicesCacheHandler,
    [Tag(All)] IFilterCacheHandler allServicesFilterCacheHandler,
    [Tag(Root)] IFilterCacheHandler rootServicesFilterCacheHandler,
    [Tag(Unused)] IFilterCacheHandler unusedServicesFilterCacheHandler,
    [Tag(All)] IServiceAndFilterCacheHandler allServicesAndFilterCacheHandler,
    [Tag(Root)] IServiceAndFilterCacheHandler rootServicesAndFilterCacheHandler,
    [Tag(Unused)] IServiceAndFilterCacheHandler unusedServicesAndFilterCacheHandler) : IServiceUsageVerifier
{
    public IServiceUsageVerifier FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        if (oneTimeAllServices is not null || oneTimeRootServices is not null ||
            oneTimeAllServicesFilters is not null || oneTimeRootServicesFilters is not null ||
            allServicesAndFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet ||
            rootServicesAndFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet)
        {
            var filteredAllServices =
                allServicesAndFilterCacheHandler.GetFilteredServices(oneTimeAllServices, oneTimeAllServicesFilters);

            var filteredRootServices =
                rootServicesAndFilterCacheHandler.GetFilteredServices(oneTimeRootServices, oneTimeRootServicesFilters);

            var unusedServicesUnfiltered =
                unusedServiceDetector.FindUnusedServices(filteredAllServices, filteredRootServices);

            unusedServicesAndFilterCacheHandler.ClearAndRegisterServices(unusedServicesUnfiltered);
        }

        unusedServices = unusedServicesAndFilterCacheHandler.GetFilteredServices(oneTimeUnusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeFilters<
        TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        if (oneTimeAllServices is null && oneTimeRootServices is null &&
            allServicesFilters is null && rootServicesFilters is null &&
            !allServicesAndFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet &&
            !rootServicesAndFilterCacheHandler.NewServicesOrFiltersRegisteredSinceLastGet)
        {
            unusedServices =
                filterHandler.ApplyFilters(unusedServicesCacheHandler.GetServices(), unusedServicesFilters);

            return this;
        }

        var extractedAllServices = allServicesCacheHandler.GetServices(oneTimeAllServices);
        var extractedRootServices = rootServicesCacheHandler.GetServices(oneTimeRootServices);

        var filteredAllServices = filterHandler.ApplyFilters(extractedAllServices, allServicesFilters);
        var filteredRootServices = filterHandler.ApplyFilters(extractedRootServices, rootServicesFilters);

        var unusedServicesUnfiltered =
            unusedServiceDetector.FindUnusedServices(filteredAllServices, filteredRootServices);

        unusedServices = filterHandler.ApplyFilters(unusedServicesUnfiltered, unusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServices<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        var extractedAllServices = serviceInfoExtractorHandler.GetServiceInfo(allServices);
        var extractedRootServices = serviceInfoExtractorHandler.GetServiceInfo(rootServices);

        var filteredAllServices =
            allServicesFilterCacheHandler.ApplyFilters(extractedAllServices, oneTimeAllServicesFilters);

        var filteredRootServices =
            rootServicesFilterCacheHandler.ApplyFilters(extractedRootServices, oneTimeRootServicesFilters);

        var unusedServicesUnfiltered =
            unusedServiceDetector.FindUnusedServices(filteredAllServices, filteredRootServices);

        unusedServices =
            unusedServicesFilterCacheHandler.ApplyFilters(unusedServicesUnfiltered, oneTimeUnusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        var extractedAllServices = serviceInfoExtractorHandler.GetServiceInfo(allServices);
        var extractedRootServices = serviceInfoExtractorHandler.GetServiceInfo(rootServices);

        var filteredAllServices = filterHandler.ApplyFilters(extractedAllServices, allServicesFilters);
        var filteredRootServices = filterHandler.ApplyFilters(extractedRootServices, rootServicesFilters);

        var unusedServicesUnfiltered =
            unusedServiceDetector.FindUnusedServices(filteredAllServices, filteredRootServices);

        unusedServices = filterHandler.ApplyFilters(unusedServicesUnfiltered, unusedServicesFilters);

        return this;
    }
}