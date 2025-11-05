using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterFilters
{
    IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    IServiceUsageVerifier RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters = null);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilterInfoList allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilter allServicesFilter) =>
        RegisterFilters(allServicesFilter);

    IServiceUsageVerifier RegisterAllServicesFilters(params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilterInfoList rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilter rootServicesFilter) =>
        RegisterFilters(rootServicesFilters: rootServicesFilter);

    IServiceUsageVerifier RegisterRootServicesFilters(params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilterInfoList unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilter unusedServicesFilter) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilter);

    IServiceUsageVerifier RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();
}