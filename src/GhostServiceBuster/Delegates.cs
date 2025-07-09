using GhostServiceBuster.Collections;
using GhostServiceBuster.Core;

namespace GhostServiceBuster;

public delegate ServiceInfoSet ServiceInfoExtractor<in TServiceCollection>(
    TServiceCollection serviceCollection)
    where TServiceCollection : notnull;

public delegate bool SingleServiceInfoFilter(ServiceInfo serviceInfo);

public delegate ServiceInfoSet ServiceInfoFilter(ServiceInfoSet serviceInfo);