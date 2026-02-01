using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    protected internal IServiceUsageVerifierWithoutCachesMutable
        RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull;

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull;
}

public static partial class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            IServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithoutCachesMutable
            RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
            where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
                .RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoTupleExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
            EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
            where TServiceCollectionItem : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);
    }
}