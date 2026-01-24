using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal interface IFilterCacheHandler : IFilterHandler, IFilterRegisterHandler
{
    event EventHandlerWithoutParameters NewFiltersRegistered;

    new ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters);

    new ServiceInfoSet ApplyFilters(
        ServiceInfoSet serviceInfo, IReadOnlyList<IServiceInfoFilter> oneTimeFilters) =>
        ((IFilterHandler)this).ApplyFilters(serviceInfo, oneTimeFilters);
}