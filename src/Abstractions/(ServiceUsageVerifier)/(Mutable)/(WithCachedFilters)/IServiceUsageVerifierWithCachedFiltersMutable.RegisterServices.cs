namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedFiltersMutable
{
    /// <summary>
    /// Registers all and root services, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The collection of all services.</param>
    /// <param name="rootServices">The collection of root services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterServices<TAllServicesCollection, TRootServicesCollection>(
            TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).RegisterServices(
            allServices, rootServices);

    /// <summary>
    /// Registers lazy service providers, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="getAllServicesAction">The function that provides all services.</param>
    /// <param name="getRootServicesAction">The function that provides root services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
            Func<TAllServicesCollection>? getAllServicesAction = null,
            Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).LazyRegisterServices(
            getAllServicesAction, getRootServicesAction);

    /// <summary>
    /// Registers all services, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="allServices">The collection of all services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServices<TAllServicesCollection>(
        TAllServicesCollection allServices)
        where TAllServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).RegisterAllServices(
            allServices);

    /// <summary>
    /// Registers all services lazily, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="getAllServicesAction">The function that provides all services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable LazyRegisterAllServices<TAllServicesCollection>(
        Func<TAllServicesCollection> getAllServicesAction)
        where TAllServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .LazyRegisterAllServices(getAllServicesAction);

    /// <summary>
    /// Registers root services, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="rootServices">The collection of root services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServices<TRootServicesCollection>(
        TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServices(rootServices);

    /// <summary>
    /// Registers root services lazily, returning a verifier with cached services and filters.
    /// </summary>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="getRootServicesAction">The function that provides root services.</param>
    /// <returns>The updated verifier.</returns>
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable LazyRegisterRootServices<TRootServicesCollection>(
        Func<TRootServicesCollection> getRootServicesAction)
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .LazyRegisterRootServices(getRootServicesAction);
}
