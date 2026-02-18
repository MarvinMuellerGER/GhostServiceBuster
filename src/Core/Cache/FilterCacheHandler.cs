using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Cache;

/// <summary>
/// Caches filters and applies them to service sets.
/// </summary>
internal sealed class FilterCacheHandler(IFilterHandler filterHandler) : IFilterCacheHandler
{
    private readonly List<ServiceInfoFilterInfo> _filters = [];

    /// <summary>
    /// Raised when new filters are registered.
    /// </summary>
    public event EventHandlerWithoutParameters? NewFiltersRegistered;

    /// <summary>
    /// Registers additional filters.
    /// </summary>
    /// <param name="filters">The filters to register.</param>
    public void RegisterFilters(ServiceInfoFilterInfoList? filters)
    {
        if (filters is null)
            return;

        _filters.AddRange(filters);
        NewFiltersRegistered?.Invoke();
    }

    /// <summary>
    /// Applies cached and one-time filters to the provided services.
    /// </summary>
    /// <param name="serviceInfo">The services to filter.</param>
    /// <param name="oneTimeFilters">Optional one-time filters.</param>
    /// <returns>The filtered services.</returns>
    public ServiceInfoSet ApplyFilters(ServiceInfoSet serviceInfo, ServiceInfoFilterInfoList? oneTimeFilters) =>
        filterHandler.ApplyFilters(serviceInfo, _filters.Concat(oneTimeFilters));
}
