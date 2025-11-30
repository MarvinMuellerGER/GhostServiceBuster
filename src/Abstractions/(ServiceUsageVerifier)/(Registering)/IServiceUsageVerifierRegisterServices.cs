namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterServices
{
    IServiceUsageVerifier RegisterServices<TAllServicesCollection, TRootServicesCollection>(
        TAllServicesCollection? allServices = default, TRootServicesCollection? rootServices = default)
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

    IServiceUsageVerifier RegisterRootServices<TRootServicesCollection>(TRootServicesCollection rootServices)
        where TRootServicesCollection : notnull =>
        RegisterServices<object, TRootServicesCollection>(rootServices: rootServices);

    IServiceUsageVerifier UseAllServicesAsRootServices();
}