using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

/// <summary>
/// Combines service and filter caches to provide filtered services.
/// </summary>
internal sealed class ServiceAndFilterCacheHandler : IServiceAndFilterCacheHandler
{
    private readonly IServiceCacheHandler _serviceCacheHandler;
    private readonly IFilterCacheHandler _filterCacheHandler;

    private ServiceInfoSet? _servicesFiltered = [];

    /// <summary>
    /// Gets whether services or filters were registered since the last retrieval.
    /// </summary>
    public bool NewServicesOrFiltersRegisteredSinceLastGet { get; private set; }

    /// <summary>
    /// Initializes a new instance of the cache handler.
    /// </summary>
    /// <param name="serviceCacheHandler">The service cache handler.</param>
    /// <param name="filterCacheHandler">The filter cache handler.</param>
    public ServiceAndFilterCacheHandler(
        IServiceCacheHandler serviceCacheHandler, IFilterCacheHandler filterCacheHandler)
    {
        _serviceCacheHandler = serviceCacheHandler;
        _filterCacheHandler = filterCacheHandler;

        _serviceCacheHandler.NewServicesRegistered += OnNewServicesOrFiltersRegistered;
        _filterCacheHandler.NewFiltersRegistered += OnNewServicesOrFiltersRegistered;
    }

    /// <summary>
    /// Gets filtered services using cached and optional one-time services/filters.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="oneTimeServices">Optional one-time services.</param>
    /// <param name="oneTimeFilters">Optional one-time filters.</param>
    /// <returns>The filtered services.</returns>
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
