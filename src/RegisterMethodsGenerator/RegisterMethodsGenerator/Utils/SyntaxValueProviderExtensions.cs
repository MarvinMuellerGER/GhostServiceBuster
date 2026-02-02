using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace GhostServiceBuster.RegisterMethodsGenerator.Utils;

internal static class SyntaxValueProviderExtensions
{
    public static IncrementalValuesProvider<T> ForAttributesWithMetadataNames<T>(
        this SyntaxValueProvider syntaxValueProvider,
        IEnumerable<string> fullyQualifiedMetadataNames,
        Func<SyntaxNode, CancellationToken, bool> predicate,
        Func<GeneratorAttributeSyntaxContext, CancellationToken, T> transform) =>
        fullyQualifiedMetadataNames.Select(fullyQualifiedMetadataName =>
                syntaxValueProvider.ForAttributeWithMetadataName(fullyQualifiedMetadataName, predicate, transform))
            .ConcatAll();
}