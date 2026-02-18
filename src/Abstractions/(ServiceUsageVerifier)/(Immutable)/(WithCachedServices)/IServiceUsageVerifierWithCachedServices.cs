using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

/// <inheritdoc cref="IServiceUsageVerifier" />
public interface IServiceUsageVerifierWithCachedServices
    : IServiceUsageVerifierWithCachedServicesFluently, IServiceUsageVerifierWithoutCaches
{
    ServiceInfoSet FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull
    {
        FindUnusedServicesUsingOnlyOneTimeFilters(out var unusedServices, in oneTimeAllServices, in oneTimeRootServices,
            allServicesFilters, rootServicesFilters, unusedServicesFilters);

        return unusedServices;
    }
}