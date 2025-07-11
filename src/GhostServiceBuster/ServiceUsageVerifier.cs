using GhostServiceBuster.Cache;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Extract;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

internal sealed class ServiceUsageVerifier(
    IUnusedServiceDetector unusedServiceDetector,
    IServiceInfoExtractorHandler serviceInfoExtractorHandler,
    IFilterHandler filterHandler,
    IFilterCacheHandler allServicesFilterCacheHandler,
    IFilterCacheHandler rootServicesFilterCacheHandler,
    IFilterCacheHandler unusedServicesFilterCacheHandler) : IServiceUsageVerifier
{
    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
    {
        serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

        return this;
    }

    public IServiceUsageVerifier RegisterFilters(ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null, ServiceInfoFilterInfoList? unusedServicesFilters = null)
    {
        if (allServicesFilters is not null)
            allServicesFilterCacheHandler.RegisterFilters(allServicesFilters);

        if (rootServicesFilters is not null)
            rootServicesFilterCacheHandler.RegisterFilters(rootServicesFilters);

        if (unusedServicesFilters is not null)
            unusedServicesFilterCacheHandler.RegisterFilters(unusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
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

    public IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection,
        TRootServicesCollection>(
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

        var filteredAllServices = filterHandler.ApplyFilters(extractedAllServices, allServicesFilters ?? []);
        var filteredRootServices = filterHandler.ApplyFilters(extractedRootServices, rootServicesFilters ?? []);

        var unusedServicesUnfiltered =
            unusedServiceDetector.FindUnusedServices(filteredAllServices, filteredRootServices);
        unusedServices = filterHandler.ApplyFilters(unusedServicesUnfiltered, unusedServicesFilters ?? []);

        return this;
    }
}