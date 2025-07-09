using System.Collections.Immutable;

namespace GhostServiceBuster.Filter;

public record SingleServiceInfoFilterInfo(SingleServiceInfoFilter Filter, bool IsIndividual = false);

public record ServiceInfoFilterInfo(ServiceInfoFilter Filter, bool IsIndividual = false)
{
    public static implicit operator ServiceInfoFilterInfo((ServiceInfoFilter Filter, bool IsIndividual) tuple)
        => new(tuple.Filter, tuple.IsIndividual);

    public static implicit operator ServiceInfoFilterInfo(
        (SingleServiceInfoFilter Filter, bool IsIndividual) tuple)
        => new(tuple.Filter.ToServiceInfoFilter(), tuple.IsIndividual);

    public static implicit operator ServiceInfoFilterInfo(ServiceInfoFilter filter) => new(filter);

    public static implicit operator ServiceInfoFilterInfo(SingleServiceInfoFilter filter) =>
        new(filter.ToServiceInfoFilter());

    public static implicit operator ServiceInfoFilterInfo(SingleServiceInfoFilterInfo filterInfo)
        => new(filterInfo.Filter.ToServiceInfoFilter(), filterInfo.IsIndividual);
}

file static class SingleServiceInfoFilterExtensions
{
    public static ServiceInfoFilter ToServiceInfoFilter(this SingleServiceInfoFilter filter)
        => serviceInfo =>
            serviceInfo
                .Where(filter.Invoke)
                .ToImmutableHashSet();
}