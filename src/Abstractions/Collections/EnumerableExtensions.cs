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
    /// <summary>
    /// Converts a sequence of <see cref="ServiceInfo"/> instances to a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <typeparam name="TServiceInfo">The service info subtype.</typeparam>
    /// <param name="enumerable">The source sequence.</param>
    /// <returns>A <see cref="ServiceInfoSet"/> containing the items.</returns>
    public static ServiceInfoSet ToServiceInfoSet<TServiceInfo>(this IEnumerable<TServiceInfo> enumerable)
        where TServiceInfo : ServiceInfo =>
        enumerable.Cast<ServiceInfo>().ToImmutableHashSet();

    /// <summary>
    /// Converts a list of filters into a list of filter metadata.
    /// </summary>
    /// <param name="filters">The filters to wrap.</param>
    /// <returns>A list of filter metadata.</returns>
    public static ServiceInfoFilterInfoList ToServiceInfoFilterInfoList(
        this IReadOnlyList<IServiceInfoFilter> filters) =>
        filters.Select(f => f is IRootServiceInfoFilter rf
                ? new RootServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual, rf.UseAllServices)
                : new ServiceInfoFilterInfo(f.GetFilteredServices, f.IsIndividual))
            .ToImmutableList();

    /// <summary>
    /// Projects a non-generic sequence into a <see cref="ServiceInfoSet"/>.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <param name="selector">The projection function.</param>
    /// <returns>A <see cref="ServiceInfoSet"/> containing the projected items.</returns>
    public static ServiceInfoSet Select(this IEnumerable source, Func<object?, ServiceInfo> selector) =>
        source.Select<ServiceInfo>(selector).ToImmutableHashSet();

    /// <summary>
    /// Concatenates two sequences of filter metadata.
    /// </summary>
    /// <param name="first">The first sequence.</param>
    /// <param name="second">The second sequence.</param>
    /// <returns>A concatenated list.</returns>
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
        /// <summary>
        /// Projects a sequence into a <see cref="ServiceInfoSet"/> using an index-aware selector.
        /// </summary>
        /// <param name="selector">The selector to apply.</param>
        /// <returns>A <see cref="ServiceInfoSet"/> containing the projected items.</returns>
        public ServiceInfoSet Select(Func<TSource, int, ServiceInfo> selector) =>
            Enumerable.Select(source, selector).ToImmutableHashSet();

        /// <summary>
        /// Projects and flattens a sequence into a <see cref="ServiceInfoSet"/>.
        /// </summary>
        /// <param name="selector">The selector to apply.</param>
        /// <returns>A <see cref="ServiceInfoSet"/> containing the projected items.</returns>
        public ServiceInfoSet SelectMany(Func<TSource, IEnumerable<ServiceInfo>> selector) =>
            Enumerable.SelectMany(source, selector).ToImmutableHashSet();
    }
}
