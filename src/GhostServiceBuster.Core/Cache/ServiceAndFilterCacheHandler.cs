using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal sealed class ServiceAndFilterCacheHandler : IServiceAndFilterCacheHandler
{
    private readonly IFilterCacheHandler _filterCacheHandler;
    private readonly IServiceCacheHandler _serviceCacheHandler;

    private bool _newFiltersRegisteredSinceLastGet;
    private bool _newServicesRegisteredSinceLastGet;
    private ServiceInfoSet? _servicesFiltered = [];

    public ServiceAndFilterCacheHandler(
        IServiceCacheHandler serviceCacheHandler, IFilterCacheHandler filterCacheHandler)
    {
        _serviceCacheHandler = serviceCacheHandler;
        _filterCacheHandler = filterCacheHandler;

        _serviceCacheHandler.NewServicesRegistered += () => _newServicesRegisteredSinceLastGet = true;
        _filterCacheHandler.NewFiltersRegistered += () => _newFiltersRegisteredSinceLastGet = true;
    }

    public bool NewServicesOrFiltersRegisteredSinceLastGet =>
        _newFiltersRegisteredSinceLastGet || _newServicesRegisteredSinceLastGet;

    public void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull =>
        _serviceCacheHandler.ClearAndRegisterServices(services);

    public void RegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull =>
        _serviceCacheHandler.RegisterServices(services);

    public ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices = default, ServiceInfoFilterInfoList? oneTimeFilters = null)
        where TServiceCollection : notnull
    {
        if (!_newServicesRegisteredSinceLastGet && !_newFiltersRegisteredSinceLastGet &&
            _servicesFiltered is not null && oneTimeServices is null && oneTimeFilters is null)
            return _servicesFiltered;

        var services = _serviceCacheHandler.GetServices(oneTimeServices);
        var servicesFiltered = _filterCacheHandler.ApplyFilters(services, oneTimeFilters);

        if (oneTimeServices is null && oneTimeFilters is null)
        {
            _newServicesRegisteredSinceLastGet = false;
            _newFiltersRegisteredSinceLastGet = false;
            _servicesFiltered = servicesFiltered;
        }

        return servicesFiltered;
    }
}