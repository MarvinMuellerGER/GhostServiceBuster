using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
{
    new IServiceUsageVerifierWithCachedServicesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    new IServiceUsageVerifierWithCachedServicesMutable
        RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

    new IServiceUsageVerifierWithCachedServicesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    new IServiceUsageVerifierWithCachedServicesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    new IServiceUsageVerifierWithCachedServicesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull =>
        (IServiceUsageVerifierWithCachedServicesMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);
}