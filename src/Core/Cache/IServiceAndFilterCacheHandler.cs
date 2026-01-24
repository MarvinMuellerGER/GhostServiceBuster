using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal interface IServiceAndFilterCacheHandler
{
    bool NewServicesOrFiltersRegisteredSinceLastGet { get; }

    ServiceInfoSet GetFilteredServices<TServiceCollection>(
        in TServiceCollection? oneTimeServices = default, in ServiceInfoFilterInfoList? oneTimeFilters = null)
        where TServiceCollection : notnull;

    ServiceInfoSet GetFilteredServices(in ServiceInfoFilterInfoList? oneTimeFilters = null) =>
        GetFilteredServices<object>(null, oneTimeFilters);
}