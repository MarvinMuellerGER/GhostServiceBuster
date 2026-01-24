using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal sealed class ServiceAndFilterCacheHandler : IServiceAndFilterCacheHandler
{
    private readonly IServiceCacheHandler _serviceCacheHandler;
    private readonly IFilterCacheHandler _filterCacheHandler;

    private ServiceInfoSet? _servicesFiltered = [];

    public bool NewServicesOrFiltersRegisteredSinceLastGet { get; private set; }

    public ServiceAndFilterCacheHandler(
        IServiceCacheHandler serviceCacheHandler, IFilterCacheHandler filterCacheHandler)
    {
        _serviceCacheHandler = serviceCacheHandler;
        _filterCacheHandler = filterCacheHandler;

        _serviceCacheHandler.NewServicesRegistered += OnNewServicesOrFiltersRegistered;
        _filterCacheHandler.NewFiltersRegistered += OnNewServicesOrFiltersRegistered;
    }

    public ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices = default, in ServiceInfoFilterInfoList? oneTimeFilters = null)
        where TServiceCollection : notnull
    {
        if (!_serviceCacheHandler.HasAnyLazyRegisterActions &&
            !NewServicesOrFiltersRegisteredSinceLastGet &&
            _servicesFiltered is not null && oneTimeServices is null && oneTimeFilters is null)
            return _servicesFiltered;

        var services = _serviceCacheHandler.GetServices(oneTimeServices);
        var servicesFiltered = _filterCacheHandler.ApplyFilters(services, oneTimeFilters);

        if (oneTimeServices is null && oneTimeFilters is null)
        {
            NewServicesOrFiltersRegisteredSinceLastGet = false;
            _servicesFiltered = servicesFiltered;
        }

        return servicesFiltered;
    }

    private void OnNewServicesOrFiltersRegistered() => NewServicesOrFiltersRegisteredSinceLastGet = true;
}