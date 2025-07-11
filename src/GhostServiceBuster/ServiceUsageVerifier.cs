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

    public ServiceInfoSet GetUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
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

        var unusedServices = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);

        return unusedServicesFilterCacheHandler.ApplyFilters(unusedServices, oneTimeUnusedServicesFilters);
    }

    public ServiceInfoSet GetIndividualUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
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

        var unusedServices = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);

        return filterHandler.ApplyFilters(unusedServices, unusedServicesFilters ?? []);
    }
}