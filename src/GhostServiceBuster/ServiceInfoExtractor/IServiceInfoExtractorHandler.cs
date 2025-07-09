using GhostServiceBuster.Collections;

namespace GhostServiceBuster.ServiceInfoExtractor;

internal interface IServiceInfoExtractorHandler
{
    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection)
        where TServiceCollection : notnull;
}