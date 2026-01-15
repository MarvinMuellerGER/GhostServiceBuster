using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal sealed class ServiceAndFilterCacheHandler : IServiceAndFilterCacheHandler
{
    private readonly IFilterCacheHandler _filterCacheHandler;

    private bool _newFiltersRegisteredSinceLastGet;
    private bool _newServicesRegisteredSinceLastGet;
    private IServiceCacheHandler _serviceCacheHandler;
    private ServiceInfoSet? _servicesFiltered = [];

    public ServiceAndFilterCacheHandler(
        IServiceCacheHandler serviceCacheHandler, IFilterCacheHandler filterCacheHandler)
    {
        _serviceCacheHandler = serviceCacheHandler;
        _filterCacheHandler = filterCacheHandler;

        _serviceCacheHandler.NewServicesRegistered += OnNewServicesRegistered;
        _filterCacheHandler.NewFiltersRegistered += OnFiltersServicesRegistered;
    }

    public bool NewServicesOrFiltersRegisteredSinceLastGet =>
        _newFiltersRegisteredSinceLastGet || _newServicesRegisteredSinceLastGet;

    public void ReplaceServiceCacheHandler(IServiceCacheHandler serviceCacheHandler)
    {
        serviceCacheHandler.NewServicesRegistered += OnNewServicesRegistered;
        _serviceCacheHandler.NewServicesRegistered -= OnNewServicesRegistered;
        _serviceCacheHandler = serviceCacheHandler;
    }

    public void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull =>
        _serviceCacheHandler.ClearAndRegisterServices(services);

    public void RegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull =>
        _serviceCacheHandler.RegisterServices(services);

    public void LazyRegisterServices<TServiceCollection>(in Func<TServiceCollection> getServicesAction)
        where TServiceCollection : notnull
    {
        _serviceCacheHandler.LazyRegisterServices(getServicesAction);
    }

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

    private void OnNewServicesRegistered() => _newServicesRegisteredSinceLastGet = true;

    private void OnFiltersServicesRegistered() => _newFiltersRegisteredSinceLastGet = true;
}