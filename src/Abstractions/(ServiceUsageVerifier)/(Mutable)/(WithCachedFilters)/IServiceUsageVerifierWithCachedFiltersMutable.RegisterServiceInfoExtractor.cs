using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public static partial class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier)
    {
        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            IServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithCachedFiltersMutable
            RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
            where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
                .RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoTupleExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
            EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
            where TServiceCollectionItem : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);
    }
}