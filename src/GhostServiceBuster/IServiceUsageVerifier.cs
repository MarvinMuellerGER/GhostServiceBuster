using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifier : IImmutableServiceUsageVerifier
{
    IImmutableServiceUsageVerifier AsImmutable() => this;

    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;

    void RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    void RegisterAllServicesFilters(ServiceInfoFilterInfoList allServicesFilters)
        => RegisterFilters(allServicesFilters);

    void RegisterRootServicesFilters(ServiceInfoFilterInfoList rootServicesFilters)
        => RegisterFilters(rootServicesFilters: rootServicesFilters);

    void RegisterUnusedServicesFilters(ServiceInfoFilterInfoList unusedServicesFilters)
        => RegisterFilters(unusedServicesFilters: unusedServicesFilters);
}

public interface IImmutableServiceUsageVerifier
    : IServiceUsageVerifierWithoutCachedFilters, IServiceUsageVerifierWithCachedFilters;

public interface IServiceUsageVerifierWithoutCachedFilters
{
    ServiceInfoSet GetIndividualUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    ServiceInfoSet GetIndividualUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetIndividualUnusedServices(in allServices, in rootServices, allServicesFilters);

    ServiceInfoSet GetIndividualUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetIndividualUnusedServices(in allServices, in rootServices, rootServicesFilters: rootServicesFilters);

    ServiceInfoSet GetIndividualUnusedServicesWithUnusedServicesFilters
        <TAllServicesCollection, TRootServicesCollection>(
            in TAllServicesCollection allServices,
            in TRootServicesCollection rootServices,
            params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetIndividualUnusedServices(in allServices, in rootServices, unusedServicesFilters: unusedServicesFilters);
}

public interface IServiceUsageVerifierWithCachedFilters
{
    ServiceInfoSet GetUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    ServiceInfoSet GetUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeAllServicesFilters);

    ServiceInfoSet GetUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeRootServicesFilters);

    ServiceInfoSet GetUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeUnusedServicesFilters);
}