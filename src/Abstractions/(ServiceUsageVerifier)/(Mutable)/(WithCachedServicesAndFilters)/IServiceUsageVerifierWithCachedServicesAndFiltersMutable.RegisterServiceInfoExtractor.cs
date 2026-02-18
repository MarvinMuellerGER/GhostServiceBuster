using GhostServiceBuster.Extract;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesAndFiltersMutable
{
    /// <summary>
    /// Registers a service info extractor instance.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor instance.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
        IServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    /// <summary>
    /// Registers a service info extractor by type.
    /// </summary>
    /// <typeparam name="TServiceInfoExtractor">The extractor type.</typeparam>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>()
        where TServiceInfoExtractor : IServiceInfoExtractor<TServiceCollection>, new()
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor<TServiceInfoExtractor, TServiceCollection>();

    /// <summary>
    /// Registers a service info extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    /// <summary>
    /// Registers a service info tuple extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollection">The service collection type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterServiceInfoExtractor<TServiceCollection>(
        ServiceInfoTupleExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);

    /// <summary>
    /// Registers an enumerable service info extractor delegate.
    /// </summary>
    /// <typeparam name="TServiceCollectionItem">The collection item type.</typeparam>
    /// <param name="extractor">The extractor delegate.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterServiceInfoExtractor<TServiceCollectionItem>(
        EnumerableServiceInfoExtractor<TServiceCollectionItem> extractor)
        where TServiceCollectionItem : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterServiceInfoExtractor(extractor);
}
