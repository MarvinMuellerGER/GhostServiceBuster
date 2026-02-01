using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithoutCachesFluently : IServiceUsageVerifier
{
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, allServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        out ServiceInfoSet unusedServices,
        params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull =>
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(
            in allServices, in rootServices, out unusedServices, unusedServicesFilters: unusedServicesFilters);
}