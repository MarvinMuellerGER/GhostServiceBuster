using System.Collections.Immutable;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

partial class ServiceInfoFilterInfoList
{
    /// <summary>
    /// Converts a tuple to a list of filter info.
    /// </summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList((ServiceInfoFilter Filter, bool IsIndividual)? tuple) =>
        tuple is null ? [] : [tuple];

    /// <summary>
    /// Converts a tuple to a list of filter info using a single-service filter.
    /// </summary>
    /// <param name="tuple">The tuple to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(
        (SingleServiceInfoFilter Filter, bool IsIndividual)? tuple) => tuple is null ? [] : [tuple];

    /// <summary>
    /// Converts a filter delegate to a list of filter info.
    /// </summary>
    /// <param name="filter">The filter to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(ServiceInfoFilter? filter) =>
        filter is null ? [] : [filter];

    /// <summary>
    /// Converts a single-service filter delegate to a list of filter info.
    /// </summary>
    /// <param name="filter">The filter to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(SingleServiceInfoFilter? filter) =>
        filter is null ? [] : [filter];

    /// <summary>
    /// Converts single-service filter info to a list.
    /// </summary>
    /// <param name="filterInfo">The filter info to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(SingleServiceInfoFilterInfo? filterInfo) =>
        filterInfo is null ? [] : [filterInfo];

    /// <summary>
    /// Converts filter info to a list.
    /// </summary>
    /// <param name="filterInfo">The filter info to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(ServiceInfoFilterInfo? filterInfo) =>
        filterInfo is null ? [] : [filterInfo];

    /// <summary>
    /// Converts an immutable list to a <see cref="ServiceInfoFilterInfoList"/>.
    /// </summary>
    /// <param name="list">The list to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(ImmutableList<ServiceInfoFilterInfo> list) => new(list);

    /// <summary>
    /// Converts a list to a <see cref="ServiceInfoFilterInfoList"/>.
    /// </summary>
    /// <param name="list">The list to convert.</param>
    public static implicit operator ServiceInfoFilterInfoList(List<ServiceInfoFilterInfo> list) =>
        list.ToImmutableList();
}
