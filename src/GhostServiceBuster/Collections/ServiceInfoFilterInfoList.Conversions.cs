using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

partial class ServiceInfoFilterInfoList
{
    public static implicit operator ServiceInfoFilterInfoList((ServiceInfoFilter Filter, bool IsIndividual)? tuple)
        => tuple is null ? [] : [tuple];

    public static implicit operator ServiceInfoFilterInfoList(
        (SingleServiceInfoFilter Filter, bool IsIndividual)? tuple)
        => tuple is null ? [] : [tuple];

    public static implicit operator ServiceInfoFilterInfoList(ServiceInfoFilter? filter) =>
        filter is null ? [] : [filter];

    public static implicit operator ServiceInfoFilterInfoList(SingleServiceInfoFilter? filter) =>
        filter is null ? [] : [filter];

    public static implicit operator ServiceInfoFilterInfoList(SingleServiceInfoFilterInfo? filterInfo) =>
        filterInfo is null ? [] : [filterInfo];

    public static implicit operator ServiceInfoFilterInfoList(ServiceInfoFilterInfo? filterInfo) =>
        filterInfo is null ? [] : [filterInfo];
}