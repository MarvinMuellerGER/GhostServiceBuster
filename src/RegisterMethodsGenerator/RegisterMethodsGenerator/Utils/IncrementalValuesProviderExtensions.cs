using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GhostServiceBuster.RegisterMethodsGenerator.Utils;

internal static class IncrementalValuesProviderExtensions
{
    internal static IncrementalValuesProvider<T> ConcatAll<T>(
        this IEnumerable<IncrementalValuesProvider<T>> providers) =>
        providers.Aggregate((list, next) => list.Concat(next));

    private static IncrementalValuesProvider<T> Concat<T>(
        this IncrementalValuesProvider<T> provider1, IncrementalValuesProvider<T> provider2) =>
        provider1.Collect()
            .Combine(provider2.Collect())
            .SelectMany(static (item, _) => item.Left.Concat(item.Right));
}