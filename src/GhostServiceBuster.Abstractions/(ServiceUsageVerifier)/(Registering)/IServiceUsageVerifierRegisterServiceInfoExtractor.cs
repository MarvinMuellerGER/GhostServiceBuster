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
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    IServiceUsageVerifier RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull;
}