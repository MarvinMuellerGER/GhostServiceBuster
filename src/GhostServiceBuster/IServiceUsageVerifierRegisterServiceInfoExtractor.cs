using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterServiceInfoExtractor
{
    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollection>(
        Func<TServiceCollection, ServiceInfoTuple> extractor)
        where TServiceCollection : notnull;

    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollectionItem>(
        ServiceInfoExtractor<IEnumerable<TServiceCollectionItem>> extractor)
        where TServiceCollectionItem : notnull;
}