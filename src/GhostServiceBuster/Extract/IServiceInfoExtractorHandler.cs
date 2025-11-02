using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Extract;

internal interface IServiceInfoExtractorHandler
{
    void RegisterServiceInfoExtractor<TServiceCollection>(IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor<TServiceCollection>(extractor.ExtractServiceInfos);

    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    void RegisterServiceInfoExtractor<TServiceCollection>(Func<TServiceCollection, ServiceInfoTuple> extractor)
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor(new ServiceInfoExtractor<TServiceCollection>(serviceCollection =>
            new ServiceInfo(extractor(serviceCollection))));

    void RegisterServiceInfoExtractor<TServiceCollectionItem>(
        ServiceInfoExtractor<IEnumerable<TServiceCollectionItem>> extractor)
        where TServiceCollectionItem : notnull =>
        RegisterServiceInfoExtractor<IEnumerable<TServiceCollectionItem>>(extractor);

    ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection);
}