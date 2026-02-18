using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Filter;

/// <summary>
/// Represents a filter applied to a single service.
/// </summary>
/// <param name="serviceInfo">The service being evaluated.</param>
public delegate bool SingleServiceInfoFilter(ServiceInfo serviceInfo);

/// <summary>
/// Represents a filter applied to a set of services.
/// </summary>
/// <param name="serviceInfo">The services being evaluated.</param>
public delegate ServiceInfoSet ServiceInfoFilter(ServiceInfoSet serviceInfo);

/// <summary>
/// Stores a single-service filter and its metadata.
/// </summary>
/// <param name="Filter">The filter to apply.</param>
/// <param name="IsIndividual">Whether the filter targets individual services.</param>
public record SingleServiceInfoFilterInfo(SingleServiceInfoFilter Filter, bool IsIndividual = false);

/// <summary>
/// Stores a service-set filter and its metadata.
/// </summary>
/// <param name="Filter">The filter to apply.</param>
/// <param name="IsIndividual">Whether the filter targets individual services.</param>
public record ServiceInfoFilterInfo(ServiceInfoFilter Filter, bool IsIndividual = false)
{
    /// <summary>
    /// Converts a tuple to a <see cref="ServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="tuple">The tuple containing filter metadata.</param>
    public static implicit operator ServiceInfoFilterInfo((ServiceInfoFilter Filter, bool IsIndividual) tuple) =>
        new(tuple.Filter, tuple.IsIndividual);

    /// <summary>
    /// Converts a tuple to a <see cref="ServiceInfoFilterInfo"/> using a single-service filter.
    /// </summary>
    /// <param name="tuple">The tuple containing filter metadata.</param>
    public static implicit operator ServiceInfoFilterInfo((SingleServiceInfoFilter Filter, bool IsIndividual) tuple) =>
        new(tuple.Filter.ToServiceInfoFilter(), tuple.IsIndividual);

    /// <summary>
    /// Converts a filter delegate to a <see cref="ServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="filter">The filter to wrap.</param>
    public static implicit operator ServiceInfoFilterInfo(ServiceInfoFilter filter) => new(filter);

    /// <summary>
    /// Converts a single-service filter delegate to a <see cref="ServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="filter">The filter to wrap.</param>
    public static implicit operator ServiceInfoFilterInfo(SingleServiceInfoFilter filter) =>
        new(filter.ToServiceInfoFilter());

    /// <summary>
    /// Converts a single-service filter info to a <see cref="ServiceInfoFilterInfo"/>.
    /// </summary>
    /// <param name="filterInfo">The single-service filter info.</param>
    public static implicit operator ServiceInfoFilterInfo(SingleServiceInfoFilterInfo filterInfo) =>
        new(filterInfo.Filter.ToServiceInfoFilter(), filterInfo.IsIndividual);
}
