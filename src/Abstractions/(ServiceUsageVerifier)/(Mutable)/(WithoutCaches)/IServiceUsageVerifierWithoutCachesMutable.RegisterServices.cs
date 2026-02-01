namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    IServiceUsageVerifierWithCachedServicesMutable RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifierWithCachedServicesMutable
        LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
            Func<TAllServicesCollection>? getAllServicesAction = null,
            Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifierWithCachedServicesMutable RegisterAllServices<TAllServicesCollection>(
        TAllServicesCollection allServices)
        where TAllServicesCollection : notnull =>
        RegisterServices<TAllServicesCollection, object>(allServices);

    IServiceUsageVerifierWithCachedServicesMutable LazyRegisterAllServices<TAllServicesCollection>(
        Func<TAllServicesCollection> getAllServicesAction)
        where TAllServicesCollection : notnull =>
        LazyRegisterServices<TAllServicesCollection, object>(getAllServicesAction);

    IServiceUsageVerifierWithCachedServicesMutable RegisterRootServices<TRootServicesCollection>(
        TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        RegisterServices<object, TRootServicesCollection>(rootServices: rootServices);

    IServiceUsageVerifierWithCachedServicesMutable LazyRegisterRootServices<TRootServicesCollection>(
        Func<TRootServicesCollection> getRootServicesAction)
        where TRootServicesCollection : notnull =>
        LazyRegisterServices<object, TRootServicesCollection>(getRootServicesAction: getRootServicesAction);
}