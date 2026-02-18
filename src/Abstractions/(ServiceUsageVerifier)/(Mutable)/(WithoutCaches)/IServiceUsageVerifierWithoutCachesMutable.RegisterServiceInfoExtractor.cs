using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    /// <summary>
    /// Registers a service info extractor instance.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor instance.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    /// <summary>
    /// Registers a service info extractor by type.
    /// </summary>
    /// <typeparam name="TServiceInfoExtractor">The extractor type.</typeparam>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable
        RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull;

    /// <summary>
    /// Registers a service info extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    /// <summary>
    /// Registers a service info tuple extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    /// <summary>
    /// Registers an enumerable service info extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollectionItem">The collection item type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    protected internal IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull;
}

/// <summary>
/// Provides extension methods for service info extractor registration.
/// </summary>
public static partial class ServiceUsageVerifierExtensions
{
    extension(IServiceUsageVerifierWithoutCachesMutable serviceUsageVerifier)
    {
        /// <summary>
        /// Registers a service info extractor instance.
        /// </summary>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <param name="extractor">The extractor instance.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            IServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers a service info extractor by type.
        /// </summary>
        /// <typeparam name="TServiceInfoExtractor">The extractor type.</typeparam>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithoutCachesMutable
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
        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers a service info tuple extractor delegate.
        /// </summary>
        /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
        /// <param name="extractor">The extractor delegate.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollection>(
            ServiceInfoTupleExtractor<TServiceCollection> extractor)
            where TServiceCollection : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);

        /// <summary>
        /// Registers an enumerable service info extractor delegate.
        /// </summary>
        /// <typeparam name="TServiceCollectionItem">The collection item type.</typeparam>
        /// <param name="extractor">The extractor delegate.</param>
        /// <returns>The updated verifier.</returns>
        public IServiceUsageVerifierWithoutCachesMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
            EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
            where TServiceCollectionItem : notnull =>
            (IServiceUsageVerifierWithCachedFiltersMutable)serviceUsageVerifier.RegisterServiceInfoExtractor(extractor);
    }
}
