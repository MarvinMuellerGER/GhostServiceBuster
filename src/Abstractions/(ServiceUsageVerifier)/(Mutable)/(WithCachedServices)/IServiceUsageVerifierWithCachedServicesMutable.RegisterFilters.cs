using GhostServiceBuster.Collections;
using GhostServiceBuster.Filter;

namespace GhostServiceBuster;

public partial interface IServiceUsageVerifierWithCachedServicesMutable
{
    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterFilters(
        ServiceInfoFilterInfoList? allServicesFilters = null,
        ServiceInfoFilterInfoList? rootServicesFilters = null,
        ServiceInfoFilterInfoList? unusedServicesFilters = null)
        => (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
            .RegisterFilters(allServicesFilters, rootServicesFilters, unusedServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilterInfoList allServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        ServiceInfoFilter allServicesFilter, bool isIndividual = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilter, isIndividual);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> allServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilters(allServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilter(
        IServiceInfoFilter allServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilter(allServicesFilter);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterAllServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterAllServicesFilter<TServiceInfoFilter>();

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilterInfoList rootServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        ServiceInfoFilter rootServicesFilter, bool isIndividual = false, bool useAllServices = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilter, isIndividual, useAllServices);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> rootServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilters(rootServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilter(
        IServiceInfoFilter rootServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilter(rootServicesFilter);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterRootServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterRootServicesFilter<TServiceInfoFilter>();

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilterInfoList unusedServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        ServiceInfoFilter unusedServicesFilter, bool isIndividual = false) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilter, isIndividual);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilters(
        params IReadOnlyList<IServiceInfoFilter> unusedServicesFilters) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilters(unusedServicesFilters);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilter(
        IServiceInfoFilter unusedServicesFilter) =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilter(unusedServicesFilter);

    new IServiceUsageVerifierWithCachedServicesAndFiltersMutable RegisterUnusedServicesFilter<TServiceInfoFilter>()
        where TServiceInfoFilter : IServiceInfoFilter, new() =>
        (IServiceUsageVerifierWithCachedServicesAndFiltersMutable)((IServiceUsageVerifierWithoutCachesMutable)this)
        .RegisterUnusedServicesFilter<TServiceInfoFilter>();
}