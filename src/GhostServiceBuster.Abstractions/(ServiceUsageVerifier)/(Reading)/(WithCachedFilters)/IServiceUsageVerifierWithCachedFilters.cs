using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithCachedFilters : IServiceUsageVerifierWithCachedFiltersFluently
{
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServices(in allServices, in rootServices, out var unusedServices,
            oneTimeAllServicesFilters, oneTimeRootServicesFilters, oneTimeUnusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeAllServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeRootServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters(
            in allServices, in rootServices, out var unusedServices, oneTimeUnusedServicesFilters);

        return unusedServices;
    }
}