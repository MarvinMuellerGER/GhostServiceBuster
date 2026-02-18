using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedServicesAndFiltersFluently
    : IServiceUsageVerifierWithCachedFiltersFluently, IServiceUsageVerifierWithCachedServicesFluently
{
    IServiceUsageVerifier FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices,
        in TRootServicesCollection? oneTimeRootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier FindUnusedServices<TAllServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull =>
        FindUnusedServices<TAllServicesCollection, object>(out unusedServices, oneTimeAllServices, null,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

    IServiceUsageVerifier FindUnusedServices(out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null) =>
        FindUnusedServices<object>(out unusedServices, null,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

    IServiceUsageVerifier FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeAllServicesFilters);

    IServiceUsageVerifier FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeRootServicesFilters);

    IServiceUsageVerifier FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServices(
            out unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeUnusedServicesFilters);
}