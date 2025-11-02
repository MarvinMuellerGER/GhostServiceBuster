using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

public delegate ServiceInfoSet ServiceInfoExtractor<in TServiceCollection>(TServiceCollection serviceCollection)
    where TServiceCollection : notnull;

public delegate ServiceInfoTuple ServiceInfoTupleExtractor<in TServiceCollection>(TServiceCollection serviceCollection)
    where TServiceCollection : notnull;

public delegate ServiceInfoSet EnumerableServiceInfoExtractor<in TServiceCollectionItem>(
    IEnumerable<TServiceCollectionItem> serviceCollection)
    where TServiceCollectionItem : notnull;