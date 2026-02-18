using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

/// <summary>
/// Represents a service info extractor that returns a <see cref="ServiceInfoSet"/>.
/// </summary>
/// <typeparam name="TServiceCollection">The service collection type to extract from.</typeparam>
/// <param name="serviceCollection">The collection to inspect.</param>
public delegate ServiceInfoSet ServiceInfoExtractor<in TServiceCollection>(TServiceCollection serviceCollection)
    where TServiceCollection : notnull;

/// <summary>
/// Represents a service info extractor that returns a tuple projection.
/// </summary>
/// <typeparam name="TServiceCollection">The service collection type to extract from.</typeparam>
/// <param name="serviceCollection">The collection to inspect.</param>
public delegate ServiceInfoTuple ServiceInfoTupleExtractor<in TServiceCollection>(TServiceCollection serviceCollection)
    where TServiceCollection : notnull;

/// <summary>
/// Represents a service info extractor that works over enumerable items.
/// </summary>
/// <typeparam name="TServiceCollectionItem">The item type in the collection.</typeparam>
/// <param name="serviceCollection">The items to inspect.</param>
public delegate ServiceInfoSet EnumerableServiceInfoExtractor<in TServiceCollectionItem>(
    IEnumerable<TServiceCollectionItem> serviceCollection)
    where TServiceCollectionItem : notnull;
