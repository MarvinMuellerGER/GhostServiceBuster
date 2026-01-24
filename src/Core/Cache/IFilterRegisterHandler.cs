using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

internal interface IFilterRegisterHandler
{
    void RegisterFilters(ServiceInfoFilterInfoList? filters);

    void RegisterFilters(IReadOnlyList<IServiceInfoFilter>? filters) =>
        RegisterFilters(filters?.ToServiceInfoFilterInfoList());

    void RegisterFilter<TServiceInfoFilter>() where TServiceInfoFilter : IServiceInfoFilter, new() =>
        RegisterFilters([new TServiceInfoFilter()]);
}