using GhostServiceBuster.Collections;
using GhostServiceBuster.Detect;

namespace GhostServiceBuster.Extract;

internal interface IServiceInfoExtractorHandler
{
    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    void RegisterServiceInfoExtractor<TServiceCollection>(IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor<TServiceCollection>(extractor.ExtractServiceInfos);

    void RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor(new TServiceInfoExtractor());

    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        RegisterServiceInfoExtractor(new ServiceInfoExtractor<TServiceCollection>(serviceCollection =>
            new ServiceInfo(extractor(serviceCollection))));

    void RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull =>
        RegisterServiceInfoExtractor<IEnumerable<TServiceCollectionItem>>(enumerable => extractor(enumerable));

    ServiceInfoSet GetServiceInfo<TServiceCollection>(TServiceCollection serviceCollection);
}