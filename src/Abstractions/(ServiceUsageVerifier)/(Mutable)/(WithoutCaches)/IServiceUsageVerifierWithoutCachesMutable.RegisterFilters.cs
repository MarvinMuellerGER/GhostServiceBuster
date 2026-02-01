using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithoutCachesMutable
{
    IServiceUsageVerifierWithCachedFiltersMutable RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null);

    protected internal IServiceUsageVerifierWithCachedFiltersMutable RegisterFilters(
        IReadOnlyList<IServiceInfoFilter>? allServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? rootServicesFilters = null,
        IReadOnlyList<IServiceInfoFilter>? unusedServicesFilters = null);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilterInfoList allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilter allServicesFilter, bool isIndividual = false) =>
        RegisterFilters(new ServiceInfoFilterInfo(allServicesFilter, isIndividual));

    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        RegisterFilters(allServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilter(IServiceInfoFilter allServicesFilter) =>
        RegisterAllServicesFilters(allServicesFilter);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilterInfoList rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilter rootServicesFilter, bool isIndividual = false, bool useAllServices = false) =>
        RegisterFilters(
            rootServicesFilters: new RootServiceInfoFilterInfo(rootServicesFilter, isIndividual, useAllServices));

    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        RegisterFilters(rootServicesFilters: rootServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilter(IServiceInfoFilter rootServicesFilter) =>
        RegisterRootServicesFilters(rootServicesFilter);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();

    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilterInfoList unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilter unusedServicesFilter, bool isIndividual = false) =>
        RegisterFilters(unusedServicesFilters: new ServiceInfoFilterInfo(unusedServicesFilter, isIndividual));

    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        RegisterFilters(unusedServicesFilters: unusedServicesFilters);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilter(
        IServiceInfoFilter unusedServicesFilter) =>
        RegisterUnusedServicesFilters(unusedServicesFilter);

    IServiceUsageVerifierWithCachedFiltersMutable RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new();
}