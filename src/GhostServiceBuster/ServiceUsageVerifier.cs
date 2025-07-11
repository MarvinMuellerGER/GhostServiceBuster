using GhostServiceBuster.Cache;
using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;
using GhostServiceBuster.ServiceInfoExtractor;

namespace GhostServiceBuster;

internal sealed class ServiceUsageVerifier(
    ICoreServiceUsageVerifier coreServiceUsageVerifier,
    IServiceInfoExtractorHandler serviceInfoExtractorHandler,
    IFilterHandler filterHandler,
    IFilterCacheHandler allServicesFilterCacheHandler,
    IFilterCacheHandler rootServicesFilterCacheHandler,
    IFilterCacheHandler unusedServicesFilterCacheHandler) : IServiceUsageVerifier
{
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

    public IServiceUsageVerifier GetUnusedServices<TAllServicesCollection, TRootServicesCollection>(
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

        var filteredAllServices = allServicesFilterCacheHandler.ApplyFilters(extractedAllServices, oneTimeAllServicesFilters);
        var filteredRootServices = rootServicesFilterCacheHandler.ApplyFilters(extractedRootServices, oneTimeRootServicesFilters);

        var unusedServicesUnfiltered = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);
        unusedServices = unusedServicesFilterCacheHandler.ApplyFilters(unusedServicesUnfiltered, oneTimeUnusedServicesFilters);

        return this;
    }

    public IServiceUsageVerifier GetUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
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
        
        var unusedServicesUnfiltered = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);
        unusedServices = filterHandler.ApplyFilters(unusedServicesUnfiltered, unusedServicesFilters ?? []);

        return this;
    }
}