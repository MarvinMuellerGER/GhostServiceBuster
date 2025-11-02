using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterFilters
{
    IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilterInfoList allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilter allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilterInfoList rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilter rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilterInfoList unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilter unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);
}