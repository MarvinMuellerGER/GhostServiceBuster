using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public interface IServiceUsageVerifierRegisterFilters
{
    IServiceUsageVerifier RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    protected internal IServiceUsageVerifier RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters = null);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilterInfoList allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterAllServicesFilters(ServiceInfoFilter allServicesFilter, bool isIndividual = false) =>
        RegisterFilters(new ServiceInfoFilterInfo(allServicesFilter, isIndividual));

    IServiceUsageVerifier RegisterAllServicesFilters(params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifier RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifier RegisterRootServicesFilters(ServiceInfoFilterInfoList rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilters(
        ServiceInfoFilter rootServicesFilter, bool isIndividual = false, bool useAllServices = false) =>
        RegisterFilters(
            rootServicesFilters: new RootServiceInfoFilterInfo(rootServicesFilter, isIndividual, useAllServices));

    IServiceUsageVerifier RegisterRootServicesFilters(params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifier RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifier RegisterUnusedServicesFilters(ServiceInfoFilterInfoList unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilters(
        ServiceInfoFilter unusedServicesFilter, bool isIndividual = false) =>
        RegisterFilters(unusedServicesFilters: new ServiceInfoFilterInfo(unusedServicesFilter, isIndividual));

    IServiceUsageVerifier RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifier RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();
}