using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal interface IServiceAndFilterCacheHandler
{
    bool NewServicesOrFiltersRegisteredSinceLastGet { get; }

    void ClearAndRegisterServices<TServiceCollection>(in TServiceCollection services)
        where TServiceCollection : notnull;

    void RegisterServices<TServiceCollection>(in TServiceCollection services) where TServiceCollection : notnull;

    ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices = default, ServiceInfoFilterInfoList? oneTimeFilters = null)
        where TServiceCollection : notnull;

    ServiceInfoSet GetFilteredServices(ServiceInfoFilterInfoList? oneTimeFilters = null) =>
        GetFilteredServices<object>(null, oneTimeFilters);
}