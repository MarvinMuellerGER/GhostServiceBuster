namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterServices
{
    IServiceUsageVerifier RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier LazyRegisterServices<TAllServicesCollection, TRootServicesCollection>(
        Func<TAllServicesCollection>? getAllServicesAction = null,
        Func<TRootServicesCollection>? getRootServicesAction = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier RegisterAllServices<TAllServicesCollection>(
        TAllServicesCollection allServices, bool useAsRootServicesToo = false)
        where TAllServicesCollection : notnull
    {
        if (useAsRootServicesToo)
            UseAllServicesAsRootServices();

        return RegisterServices<TAllServicesCollection, object>(allServices);
    }

    IServiceUsageVerifier LazyRegisterAllServices<TAllServicesCollection>(
        Func<TAllServicesCollection> getAllServicesAction, bool useAsRootServicesToo = false)
        where TAllServicesCollection : notnull
    {
        if (useAsRootServicesToo)
            UseAllServicesAsRootServices();

        return LazyRegisterServices<TAllServicesCollection, object>(getAllServicesAction);
    }

    IServiceUsageVerifier RegisterRootServices<TRootServicesCollection>(TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        RegisterServices<object, TRootServicesCollection>(rootServices: rootServices);

    IServiceUsageVerifier LazyRegisterRootServices<TRootServicesCollection>(
        Func<TRootServicesCollection> getRootServicesAction)
        where TRootServicesCollection : notnull =>
        LazyRegisterServices<object, TRootServicesCollection>(getRootServicesAction: getRootServicesAction);

    IServiceUsageVerifier UseAllServicesAsRootServices();
}