using System.Collections;
using System.Collections.Immutable;
using GhostServiceBuster.Detect;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster.Collections;

public static class EnumerableExtensions
{
    public static ServiceInfoSet Select(this IEnumerable source, Func<object?, ServiceInfo> selector) =>
        Select<ServiceInfo>(source, selector).ToImmutableHashSet();

    public static ServiceInfoSet Select<TSource>(
        this IEnumerable<TSource> source, Func<TSource, int, ServiceInfo> selector) =>
        Enumerable.Select(source, selector).ToImmutableHashSet();

    public static ServiceInfoSet SelectMany<TSource>(
        this IEnumerable<TSource> source, Func<TSource, IEnumerable<ServiceInfo>> selector) =>
        Enumerable.SelectMany(source, selector).ToImmutableHashSet();

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
}