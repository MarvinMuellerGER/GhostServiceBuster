using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Cache;

internal interface IRootServiceAndFilterCacheHandler : IServiceAndFilterCacheHandler
{
    ServiceInfoSet GetFilteredServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices,
        in TRootServicesCollection? oneTimeRootServices,
        in ServiceInfoFilterInfoList? oneTimeFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;
}