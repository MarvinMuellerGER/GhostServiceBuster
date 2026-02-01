namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedFiltersMutable
{
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        RegisterServices<TAllServicesCollection, TRootServicesCollection>(
            TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).RegisterServices(
            allServices, rootServices);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable
        LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
            Func<TAllServicesCollection>? getAllServicesAction = null,
            Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).LazyRegisterServices(
            getAllServicesAction, getRootServicesAction);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServices<TAllServicesCollection>(
        TAllServicesCollection allServices)
        where TAllServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this).RegisterAllServices(
            allServices);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable LazyRegisterAllServices<TAllServicesCollection>(
        Func<TAllServicesCollection> getAllServicesAction)
        where TAllServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .LazyRegisterAllServices(getAllServicesAction);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServices<TRootServicesCollection>(
        TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServices(rootServices);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable LazyRegisterRootServices<TRootServicesCollection>(
        Func<TRootServicesCollection> getRootServicesAction)
        where TRootServicesCollection : notnull =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .LazyRegisterRootServices(getRootServicesAction);
}