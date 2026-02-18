namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    /// <summary>
    /// Registers all and root services, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="allServices">The collection of all services.</param>
    /// <param name="rootServices">The collection of root services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    /// <summary>
    /// Registers lazy service providers, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="getAllServicesAction">The function that provides all services.</param>
    /// <param name="getRootServicesAction">The function that provides root services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable
        LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
            Func<TAllServicesCollection>? getAllServicesAction = null,
            Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    /// <summary>
    /// Registers all services, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="allServices">The collection of all services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable RegisterAllServices<TAllServicesCollection>(
        TAllServicesCollection allServices)
        where TAllServicesCollection : notnull =>
        RegisterServices<TAllServicesCollection, object>(allServices);

    /// <summary>
    /// Registers all services lazily, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TAllServicesCollection">The collection type for all services.</typeparam>
    /// <param name="getAllServicesAction">The function that provides all services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable LazyRegisterAllServices<TAllServicesCollection>(
        Func<TAllServicesCollection> getAllServicesAction)
        where TAllServicesCollection : notnull =>
        LazyRegisterServices<TAllServicesCollection, object>(getAllServicesAction);

    /// <summary>
    /// Registers root services, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="rootServices">The collection of root services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable RegisterRootServices<TRootServicesCollection>(
        TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        RegisterServices<object, TRootServicesCollection>(rootServices: rootServices);

    /// <summary>
    /// Registers root services lazily, returning a verifier with cached services.
    /// </summary>
    /// <typeparam name="TRootServicesCollection">The collection type for root services.</typeparam>
    /// <param name="getRootServicesAction">The function that provides root services.</param>
    /// <returns>The updated verifier.</returns>
    IServiceUsageVerifierWithCachedServicesMutable LazyRegisterRootServices<TRootServicesCollection>(
        Func<TRootServicesCollection> getRootServicesAction)
        where TRootServicesCollection : notnull =>
        LazyRegisterServices<object, TRootServicesCollection>(getRootServicesAction: getRootServicesAction);
}
