using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;
using GhostServiceBuster.Filter;
using GhostServiceBuster.ServiceInfoExtractor;

namespace GhostServiceBuster;

internal sealed class ServiceUsageVerifier(
    ICoreServiceUsageVerifier coreServiceUsageVerifier,
    IServiceInfoExtractorHandler serviceInfoExtractorHandler,
    IFilterHandler filterHandler) : IServiceUsageVerifier
{
    public void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull
        => serviceInfoExtractorHandler.RegisterServiceInfoExtractor(extractor);

    public ServiceInfoSet GetUnusedServices<TServiceCollection>(
        in TServiceCollection allServices,
        in TServiceCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null) where TServiceCollection : notnull
    {
        var extractedAllServices = serviceInfoExtractorHandler.GetServiceInfo(allServices);
        var extractedRootServices = serviceInfoExtractorHandler.GetServiceInfo(rootServices);

        var filteredAllServices = filterHandler.ApplyFilters(extractedAllServices, oneTimeAllServicesFilters);
        var filteredRootServices = filterHandler.ApplyFilters(extractedRootServices, oneTimeRootServicesFilters);

        var unusedServices = coreServiceUsageVerifier.GetUnusedServices(filteredAllServices, filteredRootServices);

        return filterHandler.ApplyFilters(unusedServices, oneTimeUnusedServicesFilters);
    }
}