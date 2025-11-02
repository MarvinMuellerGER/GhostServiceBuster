using GhostServiceBuster.Collections;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierWithCachedServicesFluently
{
    IServiceUsageVerifier FindUnusedServicesUsingOnlyOneTimeFilters<TAllServicesCollection, TRootServicesCollection>(
        out ServiceInfoSet unusedServices,
        in TAllServicesCollection? oneTimeAllServices = default,
        in TRootServicesCollection? oneTimeRootServices = default,
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        where TAllServicesCollection : notnull
        where TRootServicesCollection : notnull;
}