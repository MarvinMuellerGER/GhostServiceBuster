using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public static partial class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithCachedFiltersMutable serviceUsageVerifier)
    {
        /// <summary>
        /// Registers a service info extractor instance.
        /// </summary>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <param name="extractor">The extractor instance.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            IServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers a service info extractor by type.
        /// </summary>
        /// <typeparam name="TServiceInfoExtractor">The extractor type.</typeparam>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable
            RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
            where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier
                .RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

        /// <summary>
        /// Registers a service info extractor delegate.
        /// </summary>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <param name="extractor">The extractor delegate.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers a service info tuple extractor delegate.
        /// </summary>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <param name="extractor">The extractor delegate.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoTupleExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers an enumerable service info extractor delegate.
        /// </summary>
        /// <typeparam name="TServiceCollectionItem">The collection item type.</typeparam>
        /// <param name="extractor">The extractor delegate.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithCachedFiltersMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
            EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
            where TServiceCollectionItem : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);
    }
}
