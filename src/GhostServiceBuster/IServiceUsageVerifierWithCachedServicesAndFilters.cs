using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithCachedServicesAndFilters
    : IServiceUsageVerifierWithCachedServicesAndFiltersFluently
{
    ServiceInfoSet FindUnusedServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServices(out var unusedServices, in oneTimeAllServices,
            in oneTimeRootServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServices<TAllServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
    {
        FindUnusedServices(out var unusedServices, in oneTimeAllServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServices(
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
    {
        FindUnusedServices(out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesWithAllServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithAllServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeAllServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesWithRootServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithRootServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeRootServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesWithUnusedServicesFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesWithUnusedServicesFilters(
            out var unusedServices, in oneTimeAllServices, in oneTimeRootServices, oneTimeUnusedServicesFilters);

        return unusedServices;
    }
}