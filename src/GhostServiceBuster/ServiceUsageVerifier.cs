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
    IFilterCacheHandler filterCacheHandler) : IServiceUsageVerifier
{
    public void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
        => serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

    public void RegisterFilters(ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null, ServiceInfoFilterInfoList? unusedServicesFilters = null)
    {
        if (allServicesFilters is not null)
            filterCacheHandler.RegisterFilters(allServicesFilters);

        if (rootServicesFilters is not null)
            filterCacheHandler.RegisterFilters(rootServicesFilters);

        if (unusedServicesFilters is not null)
            filterCacheHandler.RegisterFilters(unusedServicesFilters);
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

        var filteredAllServices = filterCacheHandler.ApplyFilters(extractedAllServices, oneTimeAllServicesFilters);
        var filteredRootServices = filterCacheHandler.ApplyFilters(extractedRootServices, oneTimeRootServicesFilters);

        var unusedServices = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);

        return filterCacheHandler.ApplyFilters(unusedServices, oneTimeUnusedServicesFilters);
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