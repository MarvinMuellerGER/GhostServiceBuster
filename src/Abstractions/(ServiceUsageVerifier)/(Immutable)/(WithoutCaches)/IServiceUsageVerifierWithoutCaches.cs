using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithoutCaches : IServiceUsageVerifierWithoutCachesFluently
{
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFilters(in allServices, in rootServices, out var unusedServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList allServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithAllServicesFilters(
            in allServices, in rootServices, out var unusedServices, allServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList rootServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithRootServicesFilters(
            in allServices, in rootServices, out var unusedServices, rootServicesFilters);

        return unusedServices;
    }

    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters<
        TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection allServices,
        in TRootServicesCollection rootServices,
        params ServiceInfoFilterInfoList unusedServicesFilters)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeServicesAndFiltersWithUnusedServicesFilters(
            in allServices, in rootServices, out var unusedServices, unusedServicesFilters);

        return unusedServices;
    }
}