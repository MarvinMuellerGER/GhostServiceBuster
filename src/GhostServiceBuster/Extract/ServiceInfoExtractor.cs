using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

public delegate ServiceInfoSet ServiceInfoExtractor<in TServiceCollection>(TServiceCollection serviceCollection)
    where TServiceCollection : notnull;