using GhostServiceBuster.Collections;

namespace GhostServiceBuster.Extract;

internal interface IServiceInfoExtractorHandler
{
    void RegisterServiceInfoExtractor<TServiceCollection>(IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor<TServiceCollection>(extractor.ExtractServiceInfos);
    
    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection)
        where TServiceCollection : notnull;
}