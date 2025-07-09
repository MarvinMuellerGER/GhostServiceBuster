using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifier : IImmutableServiceUsageVerifier
{
    IImmutableServiceUsageVerifier AsImmutable() => this;

    void RegisterServiceInfoExtractor<TServiceCollection>(ServiceInfoExtractor<TServiceCollection> extractor)
        where TServiceCollection : notnull;
}

public interface IImmutableServiceUsageVerifier
{
    ServiceInfoSet GetUnusedServices<TServiceCollection>(
        in TServiceCollection allServices,
        in TServiceCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TServiceCollection : notnull;

    ServiceInfoSet GetUnusedServicesWithAllServicesFilters<TServiceCollection>(
        in TServiceCollection allServices,
        in TServiceCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters) where TServiceCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeAllServicesFilters);

    ServiceInfoSet GetUnusedServicesWithRootServicesFilters<TServiceCollection>(
        in TServiceCollection allServices,
        in TServiceCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters) where TServiceCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeRootServicesFilters: oneTimeRootServicesFilters);

    ServiceInfoSet GetUnusedServicesWithUnusedServicesFilters<TServiceCollection>(
        in TServiceCollection allServices,
        in TServiceCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters) where TServiceCollection : notnull =>
        GetUnusedServices(in allServices, in rootServices, oneTimeUnusedServicesFilters: oneTimeUnusedServicesFilters);
}