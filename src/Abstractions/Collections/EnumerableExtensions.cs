using System.Collections;
using System.Collections.Immutable;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

/// <summary>
///     Provides extension methods for <see cref="IEnumerable" /> combined with <see cref="ServiceInfo" />
/// </summary>
public static class EnumerableExtensions
{
    public static ServiceInfoSet ToServiceInfoSet<TServiceInfo>(this IEnumerable<TServiceInfo> enumerable)
        where TServiceInfo : ServiceInfo =>
        enumerable.Cast<ServiceInfo>().ToImmutableHashSet();

    public static ServiceInfoFilterInfoList ToServiceInfoFilterInfoList(
        this IReadOnlyList<IServiceInfoFilter> filters) =>
        filters.Select(f => f is IRootServiceInfoFilter rf
                ? new RootServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual, rf.UseAllServices)
                : new ServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual))
            .ToImmutableList();

    public static ServiceInfoSet Select(this IEnumerable source, Func<object?, ServiceInfo> selector) =>
        source.Select<ServiceInfo>(selector).ToImmutableHashSet();

    public static ServiceInfoFilterInfoList Concat(
        this IEnumerable<ServiceInfoFilterInfo> first, IEnumerable<ServiceInfoFilterInfo>? second) =>
        Enumerable.Concat(first, second ?? []).ToImmutableList();

    private static IEnumerable<TResult> Select<TResult>(this IEnumerable source, Func<object?, TResult> selector)
    {
        // ReSharper disable once LoopCanBeConvertedToQuery
        // Necessary as it would result in unwanted recursion because Linq would use this method for selection
        foreach (var item in source)
            yield return selector(item);
    }

    extension<TSource>(IEnumerable<TSource> source)
    {
        public ServiceInfoSet Select(Func<TSource, int, ServiceInfo> selector) =>
            Enumerable.Select(source, selector).ToImmutableHashSet();

        public ServiceInfoSet SelectMany(Func<TSource, IEnumerable<ServiceInfo>> selector) =>
            Enumerable.SelectMany(source, selector).ToImmutableHashSet();
    }
}