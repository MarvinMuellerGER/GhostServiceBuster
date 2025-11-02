using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithCachedFiltersFluently
{
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServices<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? oneTimeAllServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeRootServicesFilters = null,
        ServiceInfoFilterInfoList? oneTimeUnusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeAllServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeAllServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeRootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeRootServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList oneTimeUnusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServices(
            in allServices, in rootServices, out unusedServices, oneTimeUnusedServicesFilters);
}